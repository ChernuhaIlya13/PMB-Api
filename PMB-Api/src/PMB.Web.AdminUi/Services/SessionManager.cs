using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace PMB.Web.AdminUi.Services
{
    public class SessionManager
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "token";

        public static string Token;
        
        public SessionManager(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetToken()
        {
            Token = await _localStorage.GetItemAsStringAsync(TokenKey);
            return Token;
        }

        public async Task SetToken(string token)
        {
            await _localStorage.SetItemAsStringAsync(TokenKey, token);
        }
    }
}