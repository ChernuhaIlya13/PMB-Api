using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PMB.Dal.Bll;
using PMB.Dal.Bll.Services;
using PMB.Integration.PositiveBet.Abstract;
using PMB.Jobs.Mappers;
using PMB.Jobs.Models;
using PMB.Models;
using PMB.Utilities;

namespace PMB.Jobs.Services
{
    public class PositiveParsingScheduler: BackgroundScheduler
    {
        private readonly Account[] _accounts;
        private readonly ForksProvider _forksProvider;
        public PositiveParsingScheduler(IServiceProvider provider,ForksProvider forksProvider)
            : base("*/15 * * * * *", provider)
        {
            _accounts = provider.GetRequiredService<IOptions<AccountsOptions>>().Value.Accounts;
            _forksProvider = forksProvider;
        }
        
        protected override async Task Process(IServiceScope scope)
        {
            var positiveClient = scope.ServiceProvider.GetRequiredService<IPositiveClient>();
            var service = scope.ServiceProvider.GetRequiredService<UpsertForksService>();
            
            foreach (var acc in _accounts)
            {
                try
                {
                    var login  = await positiveClient.Login(acc.Username, acc.Password);
                    Console.WriteLine("Logined");
                    if (login) break;
                }
                catch (ApplicationException ex) when (ex.Message == "Аккаунт заблокирован")
                {
                    
                }
                catch (ApplicationException ex) when (ex.Message == "Токен не найден")
                {
            
                }
                catch (HttpRequestException)
                {
            
                }
            }
            try
            {
                var forks = await positiveClient.GetForks();
                Console.WriteLine($"Ну че там {forks.Count}");

                if (forks.Count > 0)
                {
                    var forksDal = forks.Select(x => x.ConvertFromFork()).ToArray();
                    foreach (var fork in forksDal)
                    {
                        _forksProvider.Forks.OnNext(fork);
                    }
                    await service.UpsertForks(forksDal.ToArray());
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}