using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PMB.Integration.PositiveBet.Abstract;
using PMB.Models.PositiveModels;
using PMB.Utilities;

namespace PMB.Integration.PositiveBet
{
    public class PositiveClient : IPositiveClient
    {
        private const string ApiUrl = "https://positivebet.com";
        
        private static string _username = "";
        private static string _password = "";
        
        private const string UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
        
        private static HttpClient _client;
        private static bool _isLogin;

        private static readonly AsyncLock Lock = new();
        
        private static async Task<HttpResponseMessage> RunClient(Func<HttpClient, Task<HttpResponseMessage>> func)
        {
            using var releaser = await Lock.LockAsync();
            var result = await func(_client);
            Thread.Sleep(5000);

            return result;
        }
        
        public async Task<bool> Login(string login, string password)
        {
            if (_isLogin && login == _username && password == _password) 
                return _isLogin;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("user-agent", UserAgent);

            var response = await _client.GetAsync(ApiUrl + "/ru/user/login");
            var body = await ValidateAndGetContent(response);
            
            var token = await PositiveParser.GetCsrfToken(body);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                throw new ApplicationException("Токен не найден");
            }

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("YII_CSRF_TOKEN", token),
                new KeyValuePair<string, string>("UserLogin[username]", login),
                new KeyValuePair<string, string>("UserLogin[password]", password),
                new KeyValuePair<string, string>("UserLogin[rememberMe]", "0"),
                new KeyValuePair<string, string>("UserLogin[rememberMe]", "1"),
                new KeyValuePair<string, string>("yt0", ""),
            });

            response = await _client.PostAsync(ApiUrl + "/ru/user/login", content);
            body = await ValidateAndGetContent(response);
            
            
            if (body.Contains("заблокирован"))
            {
                throw new ApplicationException("Аккаунт заблокирован");
            }
            
            _isLogin = body.Contains("/ru/user/logout");
            
            _username = login;
            _password = password;
            
            return _isLogin;
        }

        public async Task<List<Fork>> GetForks()
        {
            var forkList = new List<Fork>();
            
            var forksResponse = await RunClient(async client => await client.GetAsync(ApiUrl + "/ru/bets/index"));

            var body = await ValidateAndGetContent(forksResponse);
            if (!body.Contains("/ru/user/logout"))
            {
                _isLogin = false;
                
                await Login(_username, _password);
            }
            
            if (body.Contains("Нет результатов."))
                return forkList;

            
            return await PositiveParser.GetForks(body);
        }
        
        public async Task<BetData> GetBetData(Fork fork)
        {
            var collection = new[]
            {
                new KeyValuePair<string, string>("b", Convert.ToString((int) fork.FirstBet.Bookmaker)),
                new KeyValuePair<string, string>("e", fork.FirstBet.EvId),
                new KeyValuePair<string, string>("bid", fork.FirstBet.OtherData.Replace("+", "%2B")),
                new KeyValuePair<string, string>("b2", Convert.ToString((int) fork.SecondBet.Bookmaker)),
                new KeyValuePair<string, string>("e2", fork.SecondBet.EvId),
                new KeyValuePair<string, string>("bid2", fork.SecondBet.OtherData.Replace("+", "%2B")),
                new KeyValuePair<string, string>("k1", fork.K1),
                new KeyValuePair<string, string>("k2", fork.K2),
                new KeyValuePair<string, string>("elid", fork.Elid),
                new KeyValuePair<string, string>("crid", fork.CridId),
            };
            var content = new FormUrlEncodedContent(collection);
            
            var betDataResponse = await RunClient(async client => await client.PostAsync(ApiUrl + $"/bets/bet", content));
            var betsData = await ValidateAndGetContent(betDataResponse);

            if (betsData.Contains("alreadyUsed"))
            {
                throw new ApplicationException("Уже залогинен");
            }
            
            if (betsData.Contains("bet is not found."))
            {
                throw new ApplicationException("Ставка не найдена");
            }
            
            if (betsData.Contains("error: 7"))
            {
                throw new ApplicationException("Нет подписки");
            }
            
            if (betsData.Contains("error: ") && betsData.Trim().Length < 20)
            {
                throw new ApplicationException("Ошибка PositiveBet на запрос информации по вилке");
            }
            
            try
            {
                return JsonConvert.DeserializeObject<BetData>(betsData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private static async Task<string> ValidateAndGetContent(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
                throw new HttpRequestException($"Cannot fetch response frpm {message.RequestMessage.RequestUri}");

            return await message.Content.ReadAsStringAsync();
        }

        private static async Task<int> GetFilterId()
        {
            var betsResponse = await _client.GetAsync(ApiUrl + "/ru/bets/index");
            var body = await ValidateAndGetContent(betsResponse);
            
            return await PositiveParser.GetFilterId(body);
        }
    }
}