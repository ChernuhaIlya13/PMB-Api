using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Services;

namespace PMB.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private static readonly string Login;
        private static readonly string Email;
        private static readonly string Password;

        static UserServiceTests()
        {
            Login = Guid.NewGuid().ToString("N");
            Password = Guid.NewGuid().ToString("N");
            Email = $"{Login}@mail.ru";
        }
        
        public UserServiceTests()
        {
            _userService = Init.ServiceProvider.GetRequiredService<UserService>();
        }

        [TestMethod]
        public async Task CreateKey_Success()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            
            var isRegistered = await _userService.Register(new CreateUserDto
            {
                Email = Email,
                Login = Login,
                Password = Password
            }, cts.Token);

            isRegistered.Should().BeTrue();

            await _userService.CreateKey(new CreateUserKeyDto
            {
                Login = Login,
                KeyExpirationTime = DateTimeOffset.Now.AddDays(2)
            });

            var userWithKeys = await CheckKeys();
            userWithKeys.Keys.Length.Should().Be(1);

            await _userService.CreateKey(new CreateUserKeyDto
            {
                Login = Login,
                KeyExpirationTime = DateTimeOffset.Now.AddDays(3)
            });
            
            userWithKeys = await CheckKeys();
            userWithKeys.Keys.Length.Should().Be(2);
        }

        [TestMethod]
        public async Task FreezeKey_AllCases()
        {
            await _userService.CreateKey(new CreateUserKeyDto
            {
                Login = Login,
                KeyExpirationTime = DateTimeOffset.Now.AddDays(2)
            });
            
            var keys = await CheckKeys();

            var key = keys.Keys.FirstOrDefault();
            key.Should().NotBeNull();

            var isFreeze = await _userService.FreezeKey(new FreezeKeyDto
            {
                Key = key!.Key,
                Login = Login
            });

            isFreeze.Should().BeTrue();

            isFreeze = await _userService.FreezeKey(new FreezeKeyDto
            {
                Key = key!.Key,
                Login = Login
            });
            
            isFreeze.Should().BeFalse();
        }
        
        [TestMethod]
        public async Task UnFreezeKey_AllCases()
        {
            await _userService.CreateKey(new CreateUserKeyDto
            {
                Login = Login,
                KeyExpirationTime = DateTimeOffset.Now.AddDays(2)
            });
            
            var keys = await CheckKeys();

            var key = keys.Keys.FirstOrDefault();
            key.Should().NotBeNull();
            
            var isFreeze = await _userService.FreezeKey(new FreezeKeyDto
            {
                Key = key!.Key,
                Login = Login
            });

            isFreeze.Should().BeTrue();

            var isUnFreeze = await _userService.UnFreezeKey(new UnFreezeKeyDto
            {
                Key = key!.Key,
                Login = Login
            });
            
            isUnFreeze.Should().BeTrue();
            
            isUnFreeze = await _userService.UnFreezeKey(new UnFreezeKeyDto
            {
                Key = key!.Key,
                Login = Login
            });
            
            isUnFreeze.Should().BeFalse();
            
            var updatedKeys = await CheckKeys();
            var updatedKey = updatedKeys.Keys.FirstOrDefault(x => x.Key == key.Key);
            updatedKey.Should().NotBeNull();

            key.KeyExpirationTime.Should().NotBe(updatedKey!.KeyExpirationTime);
        }
        
        private async Task<UserDto> CheckKeys()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            
            var userWithKeys = await _userService.GetUserInfo(Login, true, true, cts.Token);
            
            userWithKeys.Email.Should().Be(Email);
            userWithKeys.Login.Should().Be(Login);
            userWithKeys.Keys.Should().NotBeNull().And.NotBeEmpty().And.NotContainNulls();

            return userWithKeys;
        }
    }
}