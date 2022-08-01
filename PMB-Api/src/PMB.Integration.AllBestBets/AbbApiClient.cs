using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace PMB.Integration.AllBestBets
{
    public partial class AbbApiClient
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
            },
            Error = (_, args) => args.ErrorContext.Handled = true
        };
        
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            ReadResponseAsString = true;
        }

        public async Task<ArbApiResponse> SearchAbbBets(ArbApiRequest request, CancellationToken token)
        {
            var response = await _httpClient.PostAsync("/api/v1/arbs/bot_pro_search", 
                new FormUrlEncodedContent(request.ToContent()), token);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(token);

                return JsonConvert.DeserializeObject<ArbApiResponse>(content, DefaultSettings);
            }
            return null;
        }
    }
}