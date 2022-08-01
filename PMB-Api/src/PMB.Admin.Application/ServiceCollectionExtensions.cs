using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PMB.Admin.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseAdminApi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}