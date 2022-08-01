using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Services;
using PMB.Jobs.Models;
using PMB.Utilities;

namespace PMB.Jobs.Services
{
    public class DbCleanerScheduler: BackgroundScheduler
    {
        public DbCleanerScheduler(IServiceProvider provider) : base("*/15 * * * * *" , provider)
        {
        }

        protected override async Task Process(IServiceScope scope)
        {
            var lifetimeOptions = scope.ServiceProvider.GetRequiredService<IOptions<LifetimeOptions>>();
            
            var repo = scope.ServiceProvider.GetRequiredService<ForksService>();
            var count = await repo.DeleteWithBets(new DeleteForksModel
            {
                LifetimeBefore = lifetimeOptions.Value.Lifetime
            });

            Console.WriteLine($"Forks were deleted. Count: {count}");
        }
    }
}