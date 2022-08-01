using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace PMB.Integration.AllBestBets.Temporary
{
    public class TemporaryAbbApiClient
    {
        private static readonly JsonSerializerSettings DefaultSettings = new()
        {
            NullValueHandling = NullValueHandling.Include,
            Converters = new List<JsonConverter>
            {
                new UnixDateTimeConverter()
            },
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        
        private readonly HttpClient _client;

        public TemporaryAbbApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ArbApiResponse> GetLiveForks(TemporaryRequest request)
        {
            var response = await _client.PostAsync("/api/v1/arbs/bot_pro_search", new FormUrlEncodedContent(request.ToFormContent()));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ArbApiResponse>(content, DefaultSettings);
            }

            return null;
        }
    }
}