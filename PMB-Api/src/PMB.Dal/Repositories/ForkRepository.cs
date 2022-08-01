using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PMB.Dal.Exceptions;
using PMB.Dal.Extensions;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Repositories
{
    public class ForkRepository: PmbRepository
    {

        public ForkRepository(string connectionString) : base(connectionString) { }

        public async Task<V1ForkDal[]> SelectAsync(SelectForksQueryModel query)
        {
            var sql = @"
                select * 
                from fork f
                join bet b1
                on b1.id = f.first_bet_id
                join bet b2 
                on b2.id = f.second_bet_id
            ";
            
            var param = new DynamicParameters();
            var conditions = new List<string>();

            if (query.Bookmakers?.Any() == true)
            {
                param.Add("Bookmakers", query.Bookmakers);
                conditions.Add("f.bookmakers = ANY(@Bookmakers)");
            }
            if (query.Sports?.Any() == true)
            {
                param.Add("Sports", query.Sports);
                conditions.Add("f.sport = ANY(@Sports)");
            }
            if (query.BetTypes?.Any() == true)
            {
                param.Add("BetTypes", query.BetTypes);
                conditions.Add("f.bet_type = ANY(@BetTypes)");
            }
            if (query.CridIds?.Any() == true)
            {
                param.Add("CridIds", query.CridIds);
                conditions.Add("f.crid_id = ANY(@CridIds)");
            }

            if (conditions.Count == 0)
            {
                throw new NoDbParametersException();
            }

            sql += " where " + string.Join(" and ", conditions);
            
            var c = await GetConnAsync();
            
            var result = await c.QueryAsync<V1ForkDal, V1BetDal, V1BetDal, V1ForkDal>(
                sql,
                (fork, firstBet, secondBet) => {
                    fork.FirstBet = firstBet;
                    fork.SecondBet = secondBet;
                    return fork;
                }, splitOn: "id", param: param);

            return result.ToArray();
        }

        public async Task<V1ForkDal[]> SelectAsync(ForksFilterQueryModel filters)
        {
            var sql = @"
            select * from fork f
            join bet b1
            on b1.id = f.first_bet_id
            join bet b2
            on b2.id = f.second_bet_id
            ";
            
            var c = await GetConnAsync();
            
            var param = new DynamicParameters();
            
            var conditions = new List<string>();

            if (filters.Profit != null)
            {
                conditions.Add("f.profit >= @StartValueCoef and f.profit < @FinishValueCoef");
                param.Add("StartValueCoef", filters.Profit.Start);
                param.Add("FinishValueCoef", filters.Profit.Finish);
            }

            if (filters.TimeOfLife != null)
            {
                conditions.Add("f.lifetime >= @StartValueTimeOfLife and f.lifetime < @FinishValueTimeOfLife");
                param.Add("StartValueTimeOfLife", filters.TimeOfLife.Start);
                param.Add("FinishValueTimeOfLife", filters.TimeOfLife.Finish);
            }

            if (filters.Coefficient != null)
            {
                conditions.Add("b1.coefficient >= @StartValueCoefficient and b1.coefficient < @FinishValueCoefficient");
                conditions.Add("b2.coefficient >= @StartValueCoefficient and b2.coefficient < @FinishValueCoefficient");
                param.Add("StartValueCoefficient", filters.Coefficient.Start);
                param.Add("FinishValueCoefficient", filters.Coefficient.Finish);
            }
            if (filters.Bookmakers != null)
            {
                conditions.Add("b1.bookmaker = ANY(@Bookmakers)");
                conditions.Add("b2.bookmaker = ANY(@Bookmakers)");
                param.Add("Bookmakers", filters.Bookmakers.ToArray());
            }

            if (conditions.Count == 0)
            {
                throw new NoDbParametersException();
            }

            sql += " where " + string.Join(" and ", conditions);
            
            var result = await c.QueryAsync<V1ForkDal, V1BetDal, V1BetDal, V1ForkDal>(sql,
                (fork, firstBet, secondBet) =>
                {
                    fork.FirstBet = firstBet;
                    fork.SecondBet = secondBet;
                    return fork;
                },splitOn:"id",param:param);
            
            return result.ToArray();
;        }

        public async Task UpdateAsync(V1ForkDal[] dal, IDbTransaction tr = null)
        {
            var sql = @"
                UPDATE fork SET
                    lifetime = source.lifetime,
                    profit = source.profit
                FROM UNNEST(@Forks) source
                WHERE fork.id = source.id;";
            
            var param = new
            {
               Forks = dal
            };
            
            var c = await GetConnAsync();
            await c.ExecuteAsync(sql, param, tr);
        }

        public async Task<V1ForkDal[]> InsertAsync(V1ForkDal[] forks, IDbTransaction tr = null)
        {
            var sql = @"
        INSERT INTO fork
            (fork_id, update_count, lifetime,
             profit, sport, bet_type, bookmakers,
             teams, other, created_at, first_bet_id,
             second_bet_id, crid_id, k1, k2, el_id) 
        SELECT 
             fork_id, update_count, lifetime,
             profit, sport, bet_type, bookmakers,
             teams, other, created_at, first_bet_id,
             second_bet_id, crid_id, k1, k2, el_id
        FROM unnest(@Forks)
             returning id, fork_id, update_count, lifetime,
             profit, sport, bet_type, bookmakers,
             teams, other, created_at, first_bet_id,
             second_bet_id, crid_id, k1, k2, el_id";
            
            var param = new
            {
                Forks = forks
            };
            
            var c = await GetConnAsync();
            var result = await c.QueryAsync<V1ForkDal>(sql, param, tr);

            return result.ToArray();
        }

        public async Task<int> DeleteWithBets(DeleteForksModel command)
        {
            const string sql = @"
            DELETE FROM bet WHERE id IN (
                SELECT first_bet_id as id FROM fork WHERE lifetime >= @Lifetime or EXTRACT(EPOCH FROM (now() - (created_at - make_interval(secs => lifetime)))) > @Lifetime
                UNION
                SELECT second_bet_id AS id FROM fork WHERE lifetime >= @Lifetime or EXTRACT(EPOCH FROM (now() - (created_at - make_interval(secs => lifetime)))) > @Lifetime);
            DELETE FROM fork WHERE lifetime >= @Lifetime or EXTRACT(EPOCH FROM (now() - (created_at - make_interval(secs => lifetime)))) > @Lifetime;
            DELETE FROM bet WHERE id IN(
                SELECT b.id
                    FROM fork RIGHT JOIN bet b ON b.id = fork.first_bet_id OR b.id = fork.second_bet_id
                    WHERE fork.id IS NULL
                UNION
                SELECT b.id
                    FROM fork RIGHT JOIN bet b ON b.id = fork.second_bet_id OR b.id = fork.first_bet_id
                WHERE fork.id IS NULL);";
            
            var param = new
            {
                Lifetime = command.LifetimeBefore
            };
            
            var c = await GetConnAsync();
            return await c.ExecuteAsync(sql, param);
        }
    }
}