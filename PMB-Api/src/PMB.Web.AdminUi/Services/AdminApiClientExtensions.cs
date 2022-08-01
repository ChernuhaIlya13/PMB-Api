using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PMB.Web.AdminUi.Services
{
    public static class AdminApiClientExtensions
    {
        private const string Auth = "Authorization";
        
        public static async Task<T> Ensure<T>(this HttpResponseMessage response)
            where T : class
        {
            if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                        {
                            new StringEnumConverter()
                        }
                    });
            }

            return null;
        }

        public static StringContent ToContent<T>(this T request) => 
            new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        public static void RefreshAuthHeader(this HttpClient client)
        {
            client.DefaultRequestHeaders.Remove(Auth);
            client.DefaultRequestHeaders.Add(Auth, "Bearer " + SessionManager.Token);
        }
    }
}