using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMB.Integration.AllBestBets.Temporary;

namespace PMB.Integration.AllBestBets
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAbbIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<AbbApiClient>(x => configuration.Bind("Integration:AllBestBets", x));
            services.AddHttpClient<TemporaryAbbApiClient>(x => configuration.Bind("Integration:AllBestBets", x));

            return services;
        }
    }
}