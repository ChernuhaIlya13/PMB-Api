using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PMB.Integration.Currencies.Binance
{
    public class BinanceClient
    {
        private const string TickerEndpoint = "api/v3/ticker/price";
        
        private readonly HttpClient _client;

        public BinanceClient(HttpClient client) => _client = client;
        
        public async Task<CryptoRate[]> GetCryptoCurrencyRates(CancellationToken token) =>
            await _client.GetFromJsonAsync<CryptoRate[]>(TickerEndpoint, token);
    }
}