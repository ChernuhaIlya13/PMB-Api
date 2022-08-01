using Microsoft.Extensions.DependencyInjection;
using PMB.Dal.Repositories;

namespace PMB.Dal
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(_ => new ForkRepository(connectionString));
            services.AddScoped(_ => new BetRepository(connectionString));
            services.AddScoped(_ => new UserRepository(connectionString));
            services.AddScoped(_ => new UserRoleRepository(connectionString));
            services.AddScoped(_ => new KeyRepository(connectionString));
            services.AddScoped(_ => new TelegramUserRepository(connectionString));

            return services;
        }
    }
}