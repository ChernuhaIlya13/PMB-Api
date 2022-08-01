using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PMB.Client
{
    public abstract class ApiClient
    {
        protected HttpClient Client;

        protected ApiClient(HttpClient client)
        {
            Client = client;
        }

        protected virtual async Task<T> EnsureSuccess<T>(HttpResponseMessage response)
        {
            response = response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}