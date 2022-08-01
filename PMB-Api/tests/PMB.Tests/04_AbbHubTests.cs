using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Abb.Client.Client;
using PMB.Abb.Client.Providers;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Services;
using PMB.WebApi.Extensions;

namespace PMB.Tests;

[TestClass]
public class AbbHubTests 
{
    private readonly UserService _userService;
    private readonly KeyService _keyService;
    private readonly IAbbSignalRClient _abbClient;
    private readonly IAbbForksProvider _forksProvider;
    
    private static readonly string Login;
    private static readonly string Email;
    private static readonly string Password;

    static AbbHubTests()
    {
        Login = Guid.NewGuid().ToString("N");
        Password = Guid.NewGuid().ToString("N");
        Email = $"{Login}@mail.ru";
    }
        
    public AbbHubTests()
    {
        _userService = Init.ServiceProvider.GetRequiredService<UserService>();
        _keyService = Init.ServiceProvider.GetRequiredService<KeyService>();
        _abbClient = Init.ServiceProvider.GetRequiredService<IAbbSignalRClient>();
        _forksProvider = Init.ServiceProvider.GetRequiredService<IAbbForksProvider>();
    }
    
    [TestMethod]
    [Ignore]
    public async Task TestForks_Arrived_Success()
    {
        var isRegistered = await _userService.Register(new CreateUserDto
        {
            Email = Email,
            Login = Login,
            Password = Password
        }, CancellationToken.None);

        isRegistered.Should().BeTrue();

        await _userService.CreateKey(new CreateUserKeyDto
        {
            Login = Login,
            KeyExpirationTime = DateTimeOffset.Now.AddDays(2)
        });

        var userWithKeys = await _userService.GetUserInfo(Login, true, true, CancellationToken.None);
        userWithKeys.Keys.Length.Should().Be(1);

        var keyDto = userWithKeys.Keys.First();
        var botIdentity = await _keyService.GetBotIdentity(new SelectKeyModel
        {
            Key = keyDto.Key
        });

        await _abbClient.Start(botIdentity.GenerateToken(), CancellationToken.None);
        var firstForkArrived = await _forksProvider.ForksReceived.FirstAsync().ToTask(CancellationToken.None);
        if (firstForkArrived)
        {
            _forksProvider.Forks.Count.Should().BeGreaterThan(0);
        }
    }
}