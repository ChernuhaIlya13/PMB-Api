using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using PMB.Abb.Client.Providers;
using PMB.Abb.Models;
using PMB.Abb.Models.Models;

namespace PMB.Abb.Client.Client
{
    internal sealed class AbbSignalRClient: IAbbSignalRClient
    {
        private readonly AbbProviderOptions _abbProviderOptions;
        private readonly IAbbForksProvider _abbForksProvider;
        private HubConnection _connection;

        public AbbSignalRClient(IOptions<AbbProviderOptions> abbProviderOptions, IAbbForksProvider abbForksProvider)
        {
            _abbForksProvider = abbForksProvider;
            _abbProviderOptions = abbProviderOptions?.Value ?? throw new ArgumentNullException(Consts.ExceptionMessages.AbbOptionsMustBeProvided);
            if (string.IsNullOrEmpty(_abbProviderOptions.Url))
            {
                throw new ArgumentNullException(Consts.ExceptionMessages.HubUrlMustBeProvided);
            }
        }

        public async Task Start(string jwtToken, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new AuthenticationException(Consts.ExceptionMessages.TokenMustBeProvided);
            }
            
            _connection = new HubConnectionBuilder()
                .WithUrl($"{_abbProviderOptions.Url.Trim('/')}{Consts.AbbHubEndpoint}", options =>
                {
                    options.CloseTimeout = _abbProviderOptions.CloseTimeout;
                    options.AccessTokenProvider = () => Task.FromResult(jwtToken);
                })
                .WithAutomaticReconnect(_abbProviderOptions.RetryTimeouts)
                .Build();
            
            _connection.On<AbbFork>(Consts.AbbForks, fork =>
            {
                _abbForksProvider.AbbForks.OnNext(fork);
            });
            
            _connection.Reconnected += async (sender) =>
            {
                Console.WriteLine("Реконнект к SignalR");
            };
            _connection.Closed += async exception =>
            {
                Console.WriteLine("Соединение закрылось в SignalR");
            };
            _connection.Reconnecting += async (sender) =>
            {
                Console.WriteLine("Переподключение к серверу SignalR");
            };
            
            await _connection.StartAsync(token);
        }

        public void Dispose() => _abbForksProvider?.Dispose();
    }
}