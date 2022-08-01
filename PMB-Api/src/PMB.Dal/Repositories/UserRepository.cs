using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using PMB.Dal.Exceptions;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Repositories
{
    public class UserRepository: PmbRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public async Task Create(UserDal user, IDbTransaction transaction, CancellationToken token)
        {
            const string sql = @"
                insert into users (login, email, password_hash) 
                    values (@Login, @Email, @PasswordHash)
                ";
            
            var param = new
            {
                Login = user.Login.ToLower(),
                Email = user.Email.ToLower(),
                user.PasswordHash
            };
            
            var c = await GetConnAsync();

            await c.ExecuteAsync(new CommandDefinition(sql, param, transaction,cancellationToken:token));
        }

        public async Task<UserDal> GetByQuery(SelectUserQueryModel query, CancellationToken token)
        {
            var sql = "select * from users";

            var param = new DynamicParameters();
            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(query.Login))
            {
                param.Add("Login", query.Login);
                conditions.Add("login = @Login");
            }

            if (conditions.Count == 0)
            {
                throw new NoDbParametersException();
            }

            var condStr = string.Join(" or ", conditions);
            
            sql += $" where {condStr} ;";

            if (query.IncludeKeys)
            {
                sql += $"select * from user_key where {condStr} ;";
            }

            if (query.IncludeRoles)
            {
                sql += $"select * from user_roles where {condStr} ;";
            }

            var c = await GetConnAsync();
            
            using var multi = await c.QueryMultipleAsync(new CommandDefinition(sql, param, cancellationToken: token))
                .ConfigureAwait(false);
            
            var user = await multi.ReadFirstOrDefaultAsync<UserDal>();

            if (user == null)
                return null;
            
            if (query.IncludeKeys)
            {
                user.Keys = (await multi.ReadAsync<KeyDal>()).ToList();
            }

            if (query.IncludeRoles)
            {
                user.Roles = (await multi.ReadAsync<UserRoleDal>()).ToList();
            }

            return user;
        }

        public async Task<KeyDal[]> UserWithKeysQuery(UserWithKeysQueryModel model, CancellationToken token)
        {
            const string sql = @"
                select 
                       ur.login,
                       uk.key,
                       uk.key_expiration_time, 
                       uk.freeze_time
                from user_roles ur
                   left join user_key uk on uk.login = ur.login 
                where ur.role = ANY(@Roles)";

            var param = new
            {
                model.Roles
            };
            
            var c = await GetConnAsync();
            return (await c.QueryAsync<KeyDal>(new CommandDefinition(sql, param, cancellationToken: token)))
                .ToArray();
        }
    }
}