using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PMB.Abb.Models;
using PMB.Abb.Models.Models;
using PMB.Dal.Bll.Hubs;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PMB.Dal.Bll.Extensions;
using PMB.Dal.Bll.Hubs.Helpers;
using PMB.Dal.Bll.Mappers;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;
using PMB.Integration.PositiveBet.Abstract;
using PMB.Models.PositiveModels;
using DelegateHelper = PMB.Dal.Bll.Hubs.Helpers.DelegateHelper;

namespace PMB.Dal.Bll
{
    public class ForksProvider: IDisposable
    {
        public Subject<V1ForkDal> Forks { get; } = new();

        public Subject<AbbFork> AbbForks { get; } = new();

        public void Provide(List<AbbFork> forks)
        {
            forks.ForEach(x => AbbForks.OnNext(x));
        }
       
        private readonly IServiceProvider _provider;
        
        public ForksProvider(IServiceProvider provider)
        {
            _provider = provider;
            AbbForks.Subscribe(x =>
            {
                _provider.GetRequiredService<IHubContext<AbbForksHub>>()
                    .Clients.All.SendAsync(Consts.AbbForks, x);
            });
            Forks.AsObservable().Subscribe(async fork =>
            {
                using var scope = provider.CreateScope();

                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
                var keys = cache.GetKeys().Where(x => !x.StartsWith("CbrRates"));
                var positiveClient = scope.ServiceProvider.GetRequiredService<IPositiveClient>();
                var hub = scope.ServiceProvider.GetRequiredService<ForkHub>();
                
                // var activeUsers = keys
                //     .Select(key => cache.TryGetValue(key, out UserCacheValue user)
                //                    && user.IsAwaitingFork() ? key : null)
                //     .Where(x => x != null)
                //     .Distinct()
                //     .ToArray();
                var activeUsers = keys
                    .Select(key => cache.TryGetValue(key, out UserCacheValue user)
                                   && user.IsAwaitingFork()
                                   && DelegateHelper.ForkFilterCondition(user)(fork) ? key : null)
                    .Where(x => x != null)
                    .Distinct()
                    .ToArray();
                
                if (activeUsers.Any())
                {
                    var forkForSend = await EnrichForkWithBetData(positiveClient, fork?.ConvertToFork());

                    if (forkForSend != null)
                    {
                        var sendTasks = activeUsers.Select(key => hub.SendForkFromObservable(key, forkForSend)).ToArray();
                        await Task.WhenAll(sendTasks);
                    }
                }
            });
        }

        public async Task<Fork> CheckAndGetFork(UserCacheValue user)
        {
            using var scope = _provider.CreateScope();
            var forkRepository = scope.ServiceProvider.GetRequiredService<ForkRepository>();
            var positiveClient = scope.ServiceProvider.GetRequiredService<IPositiveClient>();

            var settings = user.Filters?.Convert();

            if (settings == null)
            {
                return null;
            }

            var selectedForks = await forkRepository.SelectAsync(settings);
            var bestFork = selectedForks.LastOrDefault(f => f.Id != user.LastForkShipped);

            if (selectedForks.Length == 0 || bestFork == null)
            {
                return null;
            }

            var forkForSend = bestFork?.ConvertToFork();
            forkForSend = await EnrichForkWithBetData(positiveClient, forkForSend);

            return forkForSend;
        }

        private static async Task<Fork> EnrichForkWithBetData(IPositiveClient positiveClient, Fork forkForSend)
        {
            if (forkForSend == null)
            {
                return null;
            }

            try
            {
                var betData = await positiveClient.GetBetData(forkForSend);

                if (betData == null)
                {
                    return null;
                }

                forkForSend.FirstBet.EvId = betData.EventId;
                forkForSend.SecondBet.EvId = betData.EventId2;
                forkForSend.FirstBet.Url = betData.Url;
                forkForSend.SecondBet.Url = betData.Url2;
                forkForSend.FirstBet.BetId = betData.BetId;
                forkForSend.SecondBet.BetId = betData.BetId2;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
           

            return forkForSend;

        }
        public void Dispose()
        {
            Forks?.Dispose();
            AbbForks?.Dispose();
        }
    }
}