using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using PMB.Admin.Domain;
using PMB.Models.V1.Requests;
using PMB.Models.V1.Responses;

namespace PMB.Web.AdminUi.Services
{
    public class AdminApiClient
    {
        private readonly HttpClient _client;
        
        private const string Endpoint = "api/v1/admin";

        public AdminApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> Ping(CancellationToken token)
        {
            _client.RefreshAuthHeader();
            try
            {
                var result = await _client.GetAsync($"{Endpoint}/ping", token);

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // ignore
            }

            return false;
        }

        public async Task<LoginResponse> Login(LoginRequest request, CancellationToken token)
        {
            _client.RefreshAuthHeader();

            try
            {
                var result = await _client.PostAsJsonAsync("api/v1/account/login", request, token);

                var response = await result.Ensure<LoginResponse>();
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Option<AllUsersQueryResult>> Users(string searchQuery, CancellationToken token)
        {
            if (searchQuery == null)
            {
                searchQuery = string.Empty;
            }
            
            _client.RefreshAuthHeader();
            try
            {
                var result = await _client.GetAsync($"{Endpoint}/users?searchQuery={searchQuery}", token);

                return await result.Ensure<Option<AllUsersQueryResult>>();
            }
            catch (Exception)
            {
                // ignore
            }
            
            return null;
        }
        
        public async Task<KeysQueryResult> Keys(string login, CancellationToken token)
        {
            _client.RefreshAuthHeader();
            try
            {
                var result = await _client.GetAsync($"{Endpoint}/keys?login={login}", token);

                var response = await result.Ensure<Option<KeysQueryResult>>();
                if (response.IsOk())
                    return response.Body;
            }
            catch (Exception)
            {
                // ignore
            }
            
            return null;
        }
        
        public async Task<KeyResult> Key(KeyQuery query, CancellationToken token)
        {
            _client.RefreshAuthHeader();
            var result = await _client.GetAsync($"{Endpoint}/key?key={query.Key}&login={query.Login}", token);
            try
            {
                var response = await result.Ensure<Option<KeyResult>>();
                if (response.IsOk())
                    return response.Body;
            }
            catch (Exception)
            {
                // ignore
            }
            
            return null;
        }
        
        public async Task<Option<KeyResult>> CreateKey(CreateKeyCommand request, CancellationToken token)
        {
            _client.RefreshAuthHeader();
            try
            {
                var result = await _client.PostAsync($"{Endpoint}/create-key", request.ToContent(), token);
                return await result.Ensure<Option<KeyResult>>();
            }
            catch (Exception e)
            {
                // ignore
            }

            return null;
        }

        public async Task<Option<KeyResult>> FreezeKey(FreezeKeyCommand request, CancellationToken token)
        {
            _client.RefreshAuthHeader();

            try
            {
                var result = await _client.PutAsync($"{Endpoint}/freeze-key", request.ToContent(), token);
                return await result.Ensure<Option<KeyResult>>();
            }
            catch (Exception e)
            {
                // ignore
            }

            return null;
        }
        
        public async Task<Option<KeyResult>> UnfreezeKey(UnfreezeKeyCommand request, CancellationToken token)
        {
            _client.RefreshAuthHeader();
            
            try
            {
                var result = await _client.PutAsync($"{Endpoint}/unfreeze-key", request.ToContent(), token);
                return await result.Ensure<Option<KeyResult>>();
            }
            catch (Exception e)
            {
                // ignore
            }

            return null;
        }
        
        public async Task<Option<KeyResult>> ChangeKeyLifetime(ChangeKeyLifetimeCommand request, CancellationToken token)
        {
            _client.RefreshAuthHeader();

            try
            {
                var result = await _client.PutAsync($"{Endpoint}/change-key-lifetime", request.ToContent(), token);
                return await result.Ensure<Option<KeyResult>>();
            }
            catch (Exception e)
            {
                // ignore
            }

            return null;
        }

        public async Task<Option<RemoveKeyResult>> RemoveKey(RemoveKeyCommand request, CancellationToken token)
        {
            _client.RefreshAuthHeader();

            try
            {
                var result = await _client.DeleteAsync($"{Endpoint}/remove-key?key={request.Key}&login={request.Login}", token);
                return await result.Ensure<Option<RemoveKeyResult>>();
            }
            catch (Exception e)
            {
                // ignore
            }

            return null;
        }
    }
}