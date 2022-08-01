using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMB.Integration.Currencies.Binance;
using PMB.Integration.Currencies.Cbr;

namespace PMB.Integration.Currencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCurrenciesIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencySettingsOptions>(x => configuration.Bind("Integration:Settings", x));
            services.AddSingleton<CbrParser>();
            services.AddHttpClient<BinanceClient>(x => configuration.Bind("Integration:Binance", x));
            services.AddHttpClient<CbrClient>(x => configuration.Bind("Integration:Cbr", x));

            return services;
        }
    }
}