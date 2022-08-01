using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PMB.Integration.Currencies.Cbr
{
    public class CbrClient
    {
        private const string RatesQuery = "scripts/XML_daily.asp?date_req={0}";
        
        private readonly HttpClient _client;
        private readonly CbrParser _cbrParser;
        
        public CbrClient(HttpClient client, CbrParser cbrParser)
        {
            _client = client;
            _cbrParser = cbrParser;
        }

        public async Task<Rate[]> GetCurrencyRates(DateTimeOffset dt, CancellationToken token)
        {
            var result = await _client.GetAsync(string.Format(RatesQuery, dt.ToString("dd/MM/yyyy")), token);
            
            return await _cbrParser.ParseCbrRates(await result.Content.ReadAsStreamAsync(token), token);
        }
    }
}