using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PMB.Dal.Bll.Authorization;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Models;
using PMB.Dal.Repositories;

namespace PMB.Dal.Bll.Services
{
    public class KeyService
    {
        private readonly KeyRepository _keyRepository;

        public KeyService(KeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public async Task<KeyDto> SelectKey(SelectKeyModel model)
        {
            var keyDal = await _keyRepository.SelectSingle(new SelectKeyQueryModel
            {
                Key = model.Key
            });

            return new KeyDto
            {
                Key = keyDal.Key,
                Login = keyDal.Login,
                FreezeTime = keyDal.FreezeTime,
                KeyExpirationTime = keyDal.KeyExpirationTime
            };
        }
        
        public async Task<ClaimsIdentity> GetBotIdentity(SelectKeyModel model)
        {
            var keyDto = await SelectKey(model);
            
            if (keyDto != null && keyDto.FreezeTime == null && keyDto.KeyExpirationTime > DateTimeOffset.Now)
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, keyDto.Key),
                    new(ClaimsIdentity.DefaultRoleClaimType, "bot")
                };
                
                var claimsIdentity = 
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                
                return claimsIdentity;
            }

            // если бота не найдено
            return null;
        }
    }
}