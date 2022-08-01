using System;
using Microsoft.Extensions.DependencyInjection;
using PMB.Abb.Client.Client;
using PMB.Abb.Client.Providers;

namespace PMB.Abb.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseAbbProvider(this IServiceCollection services, Action<AbbProviderOptions> configure)
        {
            services.Configure(configure);
            
            services.AddSingleton<IAbbForksProvider, AbbForksProvider>();
            services.AddHostedService<ForksCleanupService>();
            services.AddSingleton<IAbbSignalRClient, AbbSignalRClient>();

            return services;
        }
    }
}