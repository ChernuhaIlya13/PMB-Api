using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PMB.Models.V1.Requests;
using PMB.Models.V1.Responses;

namespace PMB.Client
{
    public class PmbApiClient: ApiClient
    {
        public PmbApiClient(HttpClient client): base(client) { }

        public async Task<BotLoginResponse> BotLogin(BotLoginRequest request, CancellationToken token = default)
        {
            var response = await Client.PostAsync("api/v1/bot/login", new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json"), token);
            return await EnsureSuccess<BotLoginResponse>(response);
        }

        public async Task<CurrencyRatesResponse> GetCurrencies(CurrencyRatesRequest request, CancellationToken token)
        {
            var response = await Client.PostAsync("api/v1/currencies/rates", new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json"), token);
            return await EnsureSuccess<CurrencyRatesResponse>(response);
        }
    }
}