using System;
using Microsoft.Extensions.DependencyInjection;
using PMB.Dal.Bll.Hubs;
using PMB.Dal.Bll.Services;

namespace PMB.Dal.Bll
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddScoped<UpsertForksService>();
            services.AddScoped<UserService>();
            services.AddScoped<KeyService>();
            services.AddScoped<ForksService>();
            services.AddScoped<CurrencyRatesService>();
            services.AddScoped<TelegramUserService>();

            services.AddSingleton<ForksProvider>();

            services.AddScoped<ForkHub>();
            services.AddScoped<AbbForksHub>();
            services.AddSignalR();

            return services;
        }
    }
}