using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using PMB.Dal.Bll.Hubs.Helpers;
using PMB.Models.Messages;
using PMB.Models.PositiveModels;

namespace PMB.Dal.Bll.Hubs
{
    [Authorize]
    public class ForkHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ForksProvider _forksProvider;
        
        public ForkHub(IMemoryCache memoryCache, ForksProvider forksProvider)
        {
            _memoryCache = memoryCache;
            _forksProvider = forksProvider;
        }
        
        public override Task OnConnectedAsync()
        {
            return Context.WithIdentity(username =>
            {
                if (!string.IsNullOrEmpty(Context.ConnectionId))
                {
                    if (!_memoryCache.TryGetValue(username, out UserCacheValue data))
                    {
                        _memoryCache.Set(username, new UserCacheValue(Context.ConnectionId, true, AwaitingFork: false));
                    }
                    else
                    {
                        _memoryCache.Set(username, data with {ConnectionId = Context.ConnectionId, IsActive = true});
                    }

                    Console.WriteLine("Connected");

                }
                
                return base.OnConnectedAsync();
            });
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Context.WithIdentity(username =>
            {
                Console.WriteLine("Disconnect");
                if (_memoryCache.TryGetValue(username, out UserCacheValue data) && data != null)
                {
                    _memoryCache.Set(username,data with { ConnectionId = null, IsActive = false, AwaitingFork = false});
                }
                return base.OnDisconnectedAsync(exception);
            });
        }

        public Task SetFilters(ForksFilterMessage filters)
        {
            return Context.WithIdentity(username =>
            {
                if (_memoryCache.TryGetValue(username, out UserCacheValue data))
                {
                    _memoryCache.Set(username, data with {Filters = filters});
                }
                return Task.CompletedTask;
            });
        }

        public Task ReadyToGetForks()
        {
            return Context.WithIdentity(async username =>
            {
                if (_memoryCache.TryGetValue(username, out UserCacheValue user) && user.IsActiveUser())
                {
                    var fork = await _forksProvider.CheckAndGetFork(user);

                    if (_memoryCache.TryGetValue(username, out user) && user != null)
                    {
                        if (fork != null && user.IsActiveUser())
                        {
                            await SendFork(username, user, fork);
                            return;
                        }

                        _memoryCache.Set(username, user with {AwaitingFork = true});
                    }
                }
            });
        }

        private async Task SendFork(string userName, UserCacheValue user, Fork fork)
        {
            try
            {
                _memoryCache.Set(userName, user with {AwaitingFork = false, LastForkShipped = fork.Id});

                await Clients.Client(user.ConnectionId).SendAsync("ReceiveForks", fork);
                Debug.WriteLine("Вилка была отправлена");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в SendFork");
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendForkFromObservable(string key, Fork fork)
        {
            try
            {
                var user = _memoryCache.Get<UserCacheValue>(key);
                if (user.IsAwaitingFork() && fork != null)
                {
                    _memoryCache.Set(key, user with {AwaitingFork = false, LastForkShipped = fork.Id});

                    await Clients.Client(user.ConnectionId).SendAsync("ReceiveForks", fork);
                    Debug.WriteLine("Вилка была отправлена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в SendForkFromObservable");
                Console.WriteLine(ex.Message);
            }
        }
    }
}