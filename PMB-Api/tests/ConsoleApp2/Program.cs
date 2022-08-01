using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMB.Abb.Client;
using PMB.Abb.Client.Client;
using PMB.Client;
using PMB.Models.V1.Requests;

namespace ConsoleApp2
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var address = "http://localhost:6002";

            var hostBuilder = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.UseAbbProvider(options => options.Url = address)
                        .AddHttpClient<PmbApiClient>(httpClient => httpClient.BaseAddress = new Uri(address)); 
                    
                }).Build();

            var provider = hostBuilder.Services;
            
            var pmbClient = provider.GetRequiredService<PmbApiClient>();
            var client = provider.GetRequiredService<IAbbSignalRClient>();

            var response = await pmbClient.BotLogin(new BotLoginRequest
            {
                Key = "key"
            }, CancellationToken.None);

            await client.Start(response.Token, CancellationToken.None);

            await hostBuilder.RunAsync();
        }
    }
}