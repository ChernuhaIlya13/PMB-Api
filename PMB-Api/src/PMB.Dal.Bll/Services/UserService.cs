using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using PMB.Dal.Bll.Authorization;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Exceptions;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;
using PMB.Models.V1.Enums;

namespace PMB.Dal.Bll.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly KeyRepository _keyRepository;
        private readonly UserRoleRepository _userRoleRepository;
        
        public UserService(UserRepository userRepository, 
            KeyRepository keyRepository, 
            UserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _keyRepository = keyRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> Register(CreateUserDto dto, CancellationToken token)
        {
            var user = await _userRepository.GetByQuery(new SelectUserQueryModel
            {
                Login = dto.Login
            }, token);

            if (user != null)
                return false;
            
            var hashedPassword = Crypto.HashPassword(dto.Password);

            try
            {
                await _userRepository.Create(new UserDal
                {
                    Login = dto.Login,
                    Email = dto.Email,
                    PasswordHash = hashedPassword
                }, null, token);

                await _userRoleRepository.CreateUserRole(new UserRoleDal
                {
                    Login = dto.Login,
                    Role = Role.PrimaryUser.ToString()
                }, null, token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task<ClaimsIdentity> GetIdentity(SelectUserDto dto, CancellationToken token)
        {
            var user = await _userRepository.GetByQuery(new SelectUserQueryModel
            {
                Login = dto.Login,
                IncludeRoles = true
            }, token);
            
            if (user != null && user.Roles?.Any() == true)
            {
                var passwordValid = Crypto.VerifyHashedPassword(user.PasswordHash, dto.Password);
                if (!passwordValid)
                {
                    throw new InvalidPasswordException();
                }

                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Roles.First().Role)
                };
                
                var claimsIdentity = 
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                
                return claimsIdentity;
            }

            // если пользователя/ролей не найдено
            throw new UserNotFoundException(dto.Login);
        }

        public async Task<bool> CreateKey(CreateUserKeyDto dto)
        {
            if (dto.KeyExpirationTime < DateTimeOffset.Now)
            {
                return false;
            }
            
            await _keyRepository.Create(new KeyDal
            {
                Key = Guid.NewGuid().ToString("N"),
                Login = dto.Login,
                KeyExpirationTime = dto.KeyExpirationTime
            });

            return true;
        }

        public async Task<UserDto> GetUserInfo(string login, bool includeKeys, bool includeRoles, CancellationToken token = default)
        {
            var result = await _userRepository.GetByQuery(new SelectUserQueryModel
            {
                Login = login,
                IncludeKeys = includeKeys,
                IncludeRoles = includeRoles
            }, token);

            return new UserDto
            {
                Login = result.Login,
                Email = result.Email,
                Keys = result.Keys.Select(x => new UserDto.KeyDto
                {
                    Key = x.Key,
                    KeyExpirationTime = x.KeyExpirationTime,
                    FreezeTime = x.FreezeTime
                }).ToArray(),
                Roles = result.Roles.Select(x => new UserDto.RoleDto
                {
                    Login = x.Login,
                    Role = Enum.Parse<Role>(x.Role)
                }).ToArray()
            };
        }

        public async Task<bool> FreezeKey(FreezeKeyDto dto)
        {
            var keys = await _keyRepository.Select(new SelectKeyQueryModel
            {
                Key = dto.Key,
                Login = dto.Login
            });

            var key = keys.FirstOrDefault();

            if (key != null && key.FreezeTime > DateTimeOffset.MinValue)
            {
                return false;
            }
            
            if (key != null)
            {
                await _keyRepository.FreezeKey(new FreezeKeyModel
                {
                    Key = dto.Key,
                    Login = dto.Login,
                    FreezeTime = DateTimeOffset.Now
                });
                return true;
            }
            return false;
        }

        public async Task<bool> UnFreezeKey(UnFreezeKeyDto dto)
        {
            var keys = await _keyRepository.Select(new SelectKeyQueryModel
            {
                Key = dto.Key,
                Login = dto.Login
            });
            
            var key = keys.FirstOrDefault();
            
            if (key?.FreezeTime > DateTimeOffset.MinValue)
            {
                await _keyRepository.UnFreezeKey(new UnFreezeKeyModel
                {
                    Key = dto.Key,
                    Login = dto.Login,
                    KeyExpirationTime = key.KeyExpirationTime.Add(DateTimeOffset.Now.Subtract(key.FreezeTime.Value))
                });
                
                return true;
            }
            
            return false;
        }
    }
}