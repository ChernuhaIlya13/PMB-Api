using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;

namespace PMB.Dal.Bll.Services
{
    public record UniqueFields(string Bookmakers, string Sport, string CridId, string BetType);
    
    public class UpsertForksService
    {
        private readonly ForkRepository _forkRepository;
        private readonly BetRepository _betRepository;

        private static readonly Func<V1BetDal, V1BetDal, bool> BetSelectCondition = (x, bet) =>
            x.Bookmaker == bet.Bookmaker && x.Coefficient == bet.Coefficient &&
            x.Direction == bet.Direction && x.Sport == bet.Sport &&
            x.Teams == bet.Teams && x.Url == bet.Url && x.BetId == bet.BetId &&
            x.BetType == bet.BetType && x.BetValue == bet.BetValue &&
            x.EvId == bet.EvId && x.ForksCount == bet.ForksCount &&
            x.MatchData == bet.MatchData &&
            x.OtherData == bet.OtherData && x.IsReq == bet.IsReq &&
            x.IsInitiator == bet.IsInitiator;

        public UpsertForksService(ForkRepository forkRepository, BetRepository betRepository)
        {
            _forkRepository = forkRepository;
            _betRepository = betRepository;
        }

        public async Task UpsertForks(V1ForkDal[] forks)
        {
            var bookmakers = forks.Select(x => x.Bookmakers).ToArray();
            var sports = forks.Select(x => x.Sport).ToArray();
            var cridIds = forks.Select(x => x.CridId).ToArray();
            var betTypes = forks.Select(x => x.BetType).ToArray();
            
            var forkFromDb = (await _forkRepository.SelectAsync(new SelectForksQueryModel
            {
                Bookmakers = bookmakers,
                Sports = sports,
                CridIds = cridIds,
                BetTypes = betTypes
            })).ToDictionary(x => new UniqueFields(x.Bookmakers, x.Sport, x.CridId, x.BetType), x => x);

            var forInsert = new List<V1ForkDal>();
            var forUpdate = new List<V1ForkDal>();

            foreach (var newFork in forks)
            {
                var uniqueFork = new UniqueFields(newFork.Bookmakers, newFork.Sport, newFork.CridId, newFork.BetType);
                if (forkFromDb.TryGetValue(uniqueFork, out var oldFork))
                {
                    if (oldFork.Lifetime != newFork.Lifetime || oldFork.Profit != newFork.Profit)
                    {
                        newFork.Id = oldFork.Id;
                        newFork.FirstBet.Id = oldFork.FirstBet.Id;
                        newFork.SecondBet.Id = oldFork.SecondBet.Id;
                        forUpdate.Add(newFork);
                    }
                }
                else
                {
                    forInsert.Add(newFork);
                }
            }

            using var tr = await _forkRepository.GetTransactionAsync();
            try
            {
                if (forInsert.Any())
                {
                    forInsert = forInsert.DistinctBy(x => (x.Bookmakers, x.CridId, x.Sport, x.BetType)).ToList();
                    
                    var bets = await _betRepository.InsertAsync(forInsert
                        .SelectMany(x => new[] {x.FirstBet, x.SecondBet})
                        .Select(x => x).ToArray(), tr);

                    foreach (var d in forInsert)
                    {
                        var firstBet = d.FirstBet;
                        var secondBet = d.SecondBet;

                        d.FirstBetId = bets.First(x => BetSelectCondition(x, firstBet)).Id;
                        d.SecondBetId = bets.First(x => BetSelectCondition(x, secondBet)).Id;
                    }

                    await _forkRepository.InsertAsync(forInsert.ToArray(), tr);
                }

                if (forUpdate.Any())
                {
                    var forksForUpdate = forUpdate.Select(x => new V1ForkDal
                    {
                        Id = x.Id,
                        Lifetime = x.Lifetime,
                        Profit = x.Profit
                    }).ToArray();

                    var betsForUpdate = forUpdate.SelectMany(x => new[] {x.FirstBet, x.SecondBet}.Select(b =>
                        new V1BetDal
                        {
                            Id = b.Id,
                            Coefficient = b.Coefficient,
                            Direction = b.Direction,
                            BetId = b.BetId,
                            ForksCount = b.ForksCount,
                            IsReq = b.IsReq,
                            MatchData = b.MatchData
                        })).ToArray();

                    await _forkRepository.UpdateAsync(forksForUpdate, tr);
                    await _betRepository.UpdateAsync(betsForUpdate, tr);
                }
                    
                tr.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tr.Rollback();
            }
        }
    }
}