using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PMB.Abb.Models.Models;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Models.PositiveModels;
using PMB.Models.V1.Responses;

namespace PMB.Jobs.Mappers
{
    public static class ModelMapper
    {
        public static AbbFork[] Convert(this Integration.AllBestBets.ArbApiResponse response)
        {
            if (response?.Arbs?.Any() != true || response?.Bets?.Any() != true)
            {
                return null;
            }
            
            return response.Arbs.Select(x =>
            {
                var fork = new AbbFork
                {
                    AbbDto = new ArbsDto
                    {
                        Id = x.Id,
                        Away = x.Away,
                        F = x.F,
                        Home = x.Home,
                        League = x.League,
                        Name = x.Name,
                        Paused = x.Paused,
                        Percent = x.Percent,
                        Roi = x.Roi,
                        ArbType = x.Arb_type,
                        AwayId = x.Away_id,
                        AdditionalProperties = x.AdditionalProperties,
                        Bet1Id = x.Bet1_id,
                        Bet2Id = x.Bet2_id,
                        Bet3Id = x.Bet3_id,
                        BkIds = x.Bk_ids?.ToArray(),
                        CornerScore = x.CornerScore,
                        CountryId = x.Country_id,
                        CreatedAt = x.Created_at,
                        EventId = x.Event_id,
                        FId = x.F_id,
                        HomeId = x.HomeId,
                        IsLive = x.Is_live,
                        LeagueId = x.League_id,
                        MaxCoefficient = x.Max_koef,
                        MiddleValue = x.Middle_value,
                        MinCoefficient = x.Min_koef,
                        SportId = x.Sport_id,
                        StartedAt = x.Started_at,
                        UpdatedAt = x.Updated_at,
                        ArbFormulaId = x.Arb_formula_id
                    },
                    Bets = new List<BetDto>
                    {
                        response.Bets.FirstOrDefault(b => b.Id == x.Bet1_id)?.Convert(),
                        response.Bets.FirstOrDefault(b => b.Id == x.Bet2_id)?.Convert(),
                    }
                };

                if (!string.IsNullOrEmpty(x.Bet3_id))
                {
                    fork.Bets.Add(response.Bets.FirstOrDefault(b => b.Id == x.Bet3_id)?.Convert());
                }

                return fork;
            }).ToArray();
        }

        private static BetDto Convert(this PMB.Integration.AllBestBets.BetDto bet)
        {
            return new()
            {
                AdditionalProperties = bet.AdditionalProperties,
                Away = bet.Away,
                Coefficient = bet.Koef,
                Diff = bet.Diff,
                Home = bet.Home,
                Id = bet.Id,
                League = bet.League,
                AwayId = bet.Away_id,
                BookmakerId = bet.Bookmaker_id,
                CoefficientLay = bet.Koef_lay,
                CurrentScore = bet.Current_score,
                DirectLink = bet.Direct_link,
                EventId = bet.Event_id,
                EventName = bet.Event_name,
                HomeId = bet.Home_id,
                IsLay = bet.Is_lay,
                LeagueId = bet.League_id,
                PeriodId = bet.Period_id,
                SportId = bet.Sport_id,
                StartedAt = bet.Started_at,
                SwapTeams = bet.Swap_teams,
                UpdatedAt = bet.Updated_at,
                BookmakerEventId = bet.Bookmaker_event_id,
                BookmakerEventName = bet.Bookmaker_event_name,
                BookmakerLeagueId = bet.Bookmaker_league_id,
                TeamAwayId = bet.Team_away_id,
                TeamHomeId = bet.Team_home_id,
                BookmakerEventDirectLink = bet.Bookmaker_event_direct_link,
                MarketAndBetType = bet.Market_and_bet_type,
                MarketAndBetTypeParam = bet.Market_and_bet_type_param
            };
        }
        
        public static V1ForkDal ConvertFromFork(this Fork fork)
        {
            var forkDal = new V1ForkDal
            {
                Bookmakers = fork.Bookmakers,
                ElId = fork.Elid,
                K1 = fork.K1,
                K2 = fork.K2,
                Lifetime = (long)fork.Lifetime.TotalSeconds,
                Other = fork.Other,
                Profit = fork.Profit,
                Sport = fork.Sport.ToString(),
                Teams = fork.Teams,
                BetType = fork.BetType,
                CreatedAt = fork.CreatedAt,
                CridId = fork.CridId,
                FirstBet = fork.FirstBet.Convert(),
                SecondBet = fork.SecondBet.Convert()
            };

            return forkDal;
        }

        private static V1BetDal Convert(this Bet bet)
        {
            return new()
            {
                Bookmaker = bet.Bookmaker.ToString(),
                Coefficient = bet.Coefficient,
                Direction = bet.Direction.ToString(),
                Parameter = bet.Parameter,
                Sport = bet.Sport.ToString(),
                Teams = bet.Teams,
                Url = bet.Url,
                BetId = bet.BetId,
                BetType = bet.BetType.ToString(),
                BetValue = bet.BetValue,
                EvId = bet.PositiveEvId,
                ForksCount = bet.ForksCount,
                IsInitiator = bet.IsInitiator,
                IsReq = bet.IsReq,
                MatchData = JsonConvert.SerializeObject(bet.MatchData),
                OtherData = bet.OtherData
            };
        }

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
    }
}