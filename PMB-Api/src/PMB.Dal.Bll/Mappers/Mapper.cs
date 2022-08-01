using System;
using System.Linq;
using Newtonsoft.Json;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Models.Messages;
using PMB.Models.Messages.Extensions;
using PMB.Models.PositiveModels;

namespace PMB.Dal.Bll.Mappers
{
    public static class Mapper
    {
        public static ForkDto Convert(this V1ForkDal fork)
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
        
        private static ForkDto.BetDto Convert(this V1BetDal bet)
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
        
        public static ForksFilterQueryModel Convert(this ForksFilterMessage query)
        {
            return new()
            {
                Coefficient = query.Coefficient != null
                    ? new ForksFilterQueryModel.Range(query.Coefficient.Start, query.Coefficient.Finish)
                    : null,
                Profit = query.Profit != null ? new ForksFilterQueryModel.Range(query.Profit.Start, query.Profit.Finish) : null,
                TimeOfLife = query.TimeOfLife != null
                    ? new ForksFilterQueryModel.Range(query.TimeOfLife.Start, query.TimeOfLife.Finish)
                    : null,
                Bookmakers = query.Bookmakers.Select(x => x.ToNormalFormat()).ToList()
            };
        }

        public static Fork ConvertToFork(this V1ForkDal value)
        {
            return new()
            {
                Elid = value.ElId,
                Id = value.Id,
                K1 = value.K1,
                K2 = value.K2,
                Lifetime = value.Lifetime >= 0 ? value.Lifetime.ConvertToLifetime() : new TimeSpan(0,0,0),
                Other = value.Other,
                Profit = value.Profit,
                Sport = value.Sport?.ConvertToSport() ?? Sport.None,
                FirstBet = value.FirstBet != null ? value.FirstBet.ConvertToBet() : new Bet(),
                SecondBet = value.SecondBet != null ? value.SecondBet.ConvertToBet() : new Bet(),
                ForkId = value.ForkId,
                UpdateCount = value.UpdateCount,
            };
        }

        private static Bet ConvertToBet(this V1BetDal value)
        {
            return new()
            {
                Bookmaker = value.Bookmaker?.ConvertToBookmaker() ?? Bookmaker.None,
                Coefficient = value.Coefficient,
                Direction = value.Direction?.ConvertToDirection() ?? Direction.None,
                Id = value.Id,
                Parameter = value.Parameter,
                Sport = value.Sport?.ConvertToSport() ?? Sport.None,
                Teams = value.Teams,
                Url = value.Url,
                BetId = value.BetId,
                BetType = value.BetType?.ConvertToBetType() ?? BetType.None,
                BetValue = value.BetValue,
                EvId = value.EvId,
                ForksCount = value.ForksCount,
                IsInitiator = value.IsInitiator,
                IsReq = value.IsReq,
                MatchData = JsonConvert.DeserializeObject<Bet.MatchDataInfo>(value.MatchData),
                OtherData = value.OtherData,
            };
        }

        private static Bookmaker ConvertToBookmaker(this string value)
        {
            return Enum.TryParse<Bookmaker>(value, out var result) ? result : Bookmaker.None;
        }

        private static TimeSpan ConvertToLifetime(this long value)
        {
            return TimeSpan.FromSeconds(value);
        }

        private static Direction ConvertToDirection(this string value)
        {
            return Enum.TryParse<Direction>(value, out var result) ? result : Direction.None;
        }

        private static Sport ConvertToSport(this string value)
        {
            return Enum.TryParse<Sport>(value, out var result) ? result : Sport.None;
        }

        private static BetType ConvertToBetType(this string value)
        {
            return Enum.TryParse<BetType>(value, out var result) ? result : BetType.None;
        }
    }
}