using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Repositories
{
    public class BetRepository: PmbRepository
    {
        public BetRepository(string connectionString): base(connectionString)
        { }
        
        public async Task UpdateAsync(V1BetDal[] dal, IDbTransaction tr = null)
        {
            var c = await GetConnAsync();

            var sql = @"
                UPDATE bet SET
                    coefficient = source.coefficient,
                    direction = source.direction,
                    forks_count = source.forks_count,
                    bet_id = source.bet_id,
                    is_req = source.is_req,
                    match_data = source.match_data
                FROM UNNEST(@Bets) source
                WHERE bet.id = source.id;";
            
            var param = new
            {
                Bets = dal
            };

            await c.ExecuteAsync(sql, param, tr);
        }

        public async Task<V1BetDal[]> InsertAsync(V1BetDal[] bets, IDbTransaction tr = null)
        {
            var sql = @"
                INSERT INTO bet
                    (bookmaker, coefficient, direction,
                     bet_type, bet_id, sport, parameter,
                     bet_value, forks_count, ev_id, other_data,
                     teams, match_data, url, is_req, is_initiator)
                SELECT 
                     bookmaker, coefficient, direction,
                     bet_type, bet_id, sport, parameter,
                     bet_value, forks_count, ev_id, other_data,
                     teams, match_data, url, is_req, is_initiator
                FROM unnest(@Bets)
                returning 
                    id, bookmaker, coefficient, direction,
                     bet_type, bet_id, sport, parameter,
                     bet_value, forks_count, ev_id, other_data,
                     teams, match_data, url, is_req, is_initiator
            ";
            
            var param = new
            {
                Bets = bets
            };
            
            var c = await GetConnAsync();

            var result = await c.QueryAsync<V1BetDal>(sql, param, tr);

            return result.ToArray();
        }
    }
}