using Microsoft.Extensions.DependencyInjection;
using PMB.Integration.PositiveBet.Abstract;

namespace PMB.Integration.PositiveBet
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPositiveIntegration(this IServiceCollection services)
        {
            services.AddSingleton<IPositiveClient, PositiveClient>();

            return services;
        }
    }
}