using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Exceptions;
using PMB.Dal.Bll.Services;
using PMB.Models.V1.Enums;
using PMB.Models.V1.Requests;
using PMB.Models.V1.Responses;
using PMB.WebApi.Extensions;

namespace PMB.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/account")]
    [SuppressMessage("Usage", "CA2200:Повторно порождайте исключения для сохранения сведений стека")]
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken token)
        {
            try
            {
                var identity = await _userService.GetIdentity(new SelectUserDto
                {
                    Login = request.Login,
                    Password = request.Password
                }, token);
                
                var response = new LoginResponse
                {
                    Token = identity.GenerateToken(),
                    Login = identity.Name
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return e switch
                {
                    InvalidPasswordException => BadRequest(e.Message),
                    UserNotFoundException => BadRequest(e.Message),
                    _ => throw e
                };
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken token)
        {
            try
            {
                var isRegistered = await _userService.Register(new CreateUserDto
                {
                    Email = request.Email,
                    Login = request.Login,
                    Password = request.Password
                }, token);

                if (isRegistered)
                    return Ok();

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = nameof(Role.System) + "," + nameof(Role.Admin))]
        [HttpPost("create-key")]
        public async Task<IActionResult> CreateKey([FromBody] AddUserKeyRequest request)
        {
            var login = HttpContext.User.Identity!.Name;
            var result = await _userService.CreateKey(new CreateUserKeyDto
            {
                Login = login,
                KeyExpirationTime = request.KeyExpirationTime
            });

            if (result)
                return Created("login", HttpContext.User.Identity!.Name);
            
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.PrimaryUser)+ "," + nameof(Role.Admin))]
        [HttpPost("get-user")]
        public async Task<UserKeyResponse> GetUser([FromBody] GetUserRequest request)
        {
            var result = await _userService.GetUserInfo(HttpContext.User.Identity!.Name, 
                request.IncludeKeys, request.IncludeRoles);

            return new UserKeyResponse
            {
                Login = result.Login,
                Email = result.Email,
                Keys = result.Keys.Select(x => new UserKeyResponse.KeyResponse
                {
                    Key = x.Key,
                    KeyExpirationTime = x.KeyExpirationTime,
                    FreezeTime = x.FreezeTime
                }).ToArray(),
                Roles = result.Roles.Select(x => x.Role.ToString()).ToArray()
            };
        }

        [Authorize(Roles = nameof(Role.PrimaryUser))]
        [HttpPost("freeze-key")]
        public async Task<IActionResult> FreezeKey([FromBody] FreezeKeyRequest request)
        {
            if (HttpContext.User.Identity!.Name == request.Login)
            {
                var result = await _userService.FreezeKey(new FreezeKeyDto
                {
                    Key = request.Key,
                    Login = request.Login
                });

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.PrimaryUser))]
        [HttpPost("unfreeze-key")]
        public async Task<IActionResult> UnFreezeKey([FromBody] UnFreezeKeyRequest request)
        {
            if (HttpContext.User.Identity!.Name == request.Login)
            {
                var result = await _userService.UnFreezeKey(new UnFreezeKeyDto
                {
                    Key = request.Key,
                    Login = request.Login
                });

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}