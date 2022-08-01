using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PMB.Dal.Bll.Authorization;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Models;
using PMB.Models.PositiveModels;
using PMB.Models.V1.Responses;

namespace PMB.WebApi.Extensions
{
    public static class ModelMapper
    {
        public static GetForksByQueryResponse.Fork Convert(this ForkDto fork)
        {
            return new()
            {
                Id = fork.Id,
                K1 = fork.K1,
                K2 = fork.K2,
                Bookmakers = fork.Bookmakers,
                Other = fork.Other,
                Profit = fork.Profit,
                Sport = fork.Sport,
                Teams = fork.Teams,
                Lifetime = fork.Lifetime,
                BetType = fork.BetType,
                CreatedAt = fork.CreatedAt,
                CridId = fork.CridId,
                ElId = fork.ElId,
                ForkId = fork.ForkId,
                UpdateCount = fork.UpdateCount,
                FirstBetId = fork.FirstBetId,
                SecondBetId = fork.SecondBetId,
                FirstBet = fork.FirstBet.Convert(),
                SecondBet = fork.SecondBet.Convert()
            };
        }

        private static GetForksByQueryResponse.Fork.Bet Convert(this ForkDto.BetDto bet)
        {
            return new()
            {
                Id = bet.Id,
                Bookmaker = bet.Bookmaker,
                Coefficient = bet.Coefficient,
                Direction = bet.Direction,
                Parameter = bet.Parameter,
                Sport = bet.Sport,
                Teams = bet.Teams,
                Url = bet.Url,
                BetId = bet.BetId,
                BetType = bet.BetType,
                BetValue = bet.BetValue,
                EvId = bet.EvId,
                ForksCount = bet.ForksCount,
                IsInitiator = bet.IsInitiator,
                IsReq = bet.IsReq,
                MatchData = bet.MatchData,
                OtherData = bet.OtherData
            };
        }
        
        public static string GenerateToken(this ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}