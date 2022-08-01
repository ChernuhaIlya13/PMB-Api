using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMB.Jobs.Models;
using PMB.Jobs.Services;

namespace PMB.Jobs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LifetimeOptions>(configuration.GetSection(nameof(LifetimeOptions)));
            services.Configure<AccountsOptions>(configuration.GetSection(nameof(AccountsOptions)));
                
            //services.AddHostedService<PositiveParsingScheduler>();
            services.AddHostedService<AbbParsingScheduler>();
            //services.AddHostedService<DbCleanerScheduler>();

            return services;
        }
    }
}