using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PMB.Web.AdminUi.Services;

namespace PMB.Web.AdminUi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddHttpClient<AdminApiClient>(c => c.BaseAddress = new Uri("http://localhost:6002"));
            
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<SessionManager>();
            builder.Services.AddMatBlazor();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
            
            await builder.Build().RunAsync();
        }
    }
}