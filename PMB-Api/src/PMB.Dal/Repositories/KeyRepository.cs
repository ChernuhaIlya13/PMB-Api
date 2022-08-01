using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using PMB.Dal.Exceptions;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;

namespace PMB.Dal.Repositories
{
    public class KeyRepository: PmbRepository
    {
        public KeyRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<KeyDal> Create(KeyDal dal)
        {
            var param = new
            {
                dal.Login,
                dal.Key,
                dal.KeyExpirationTime
            };
            
            var c = await GetConnAsync();

            return await c.QueryFirstAsync<KeyDal>(
                @"insert into user_key (login, key, key_expiration_time) 
                    values (@Login, @Key, @KeyExpirationTime)
                  returning
                    login, key, key_expiration_time
                ", param);
        }

        public async Task<KeyDal[]> Select(SelectKeyQueryModel query)
        {
            var param = new DynamicParameters();
            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(query.Login))
            {
                param.Add("Login", query.Login);
                conditions.Add("login = @Login");
            }

            if (!string.IsNullOrEmpty(query.Key))
            {
                param.Add("Key", query.Key);
                conditions.Add("key = @Key");
            }

            if (conditions.Count == 0)
            {
                throw new NoDbParametersException();
            }

            var condStr = string.Join(" and ", conditions);
            
            var c = await GetConnAsync();

            var result = await c.QueryAsync<KeyDal>(
                $"select * from user_key where {condStr}", param);

            return result.ToArray();
        }
        
        public async Task FreezeKey(FreezeKeyModel command)
        {
            var param = new
            {
                command.Login,
                command.Key,
                command.FreezeTime
            };
            
            var c = await GetConnAsync();
            
            await c.ExecuteAsync(
                @"update user_key
                    set freeze_time = @FreezeTime
                    where login = @Login and key = @Key
                ", param);
        }
        
        public async Task UnFreezeKey(UnFreezeKeyModel command)
        {
            var param = new
            {
                command.Login,
                command.Key,
                command.KeyExpirationTime
            };
            
            var c = await GetConnAsync();
            
            await c.ExecuteAsync(
                @"update user_key
                    set key_expiration_time = @KeyExpirationTime,
                        freeze_time = null
                    where login = @Login and key = @Key
                ", param);
        }
        
        public async Task UpdateKeyExpirationTime(UpdateKeyExpirationTimeModel command)
        {
            var param = new
            {
                command.Login,
                command.Key,
                command.KeyExpirationTime
            };
            
            var c = await GetConnAsync();
            
            await c.ExecuteAsync(
                @"update user_key
                    set key_expiration_time = @KeyExpirationTime
                    where login = @Login and key = @Key
                ", param);
        }

        public async Task<KeyDal> SelectSingle(SelectKeyQueryModel query)
        {
            var param = new
            {
                query.Key
            };
            
            var c = await GetConnAsync();
            
            return await c.QueryFirstOrDefaultAsync<KeyDal>(
                @"select * from user_key
                    where key = @Key
                ", param);
        }

        public async Task DeleteKey(DeleteKeyModel model, CancellationToken token)
        {
            const string sql = @"
                delete from user_key
                    where key = @Key and login = @Login
                ";
            
            var param = new
            {
                model.Key,
                model.Login
            };
            
            var c = await GetConnAsync();

            await c.ExecuteAsync(new CommandDefinition(sql, param, cancellationToken: token));
        }
    }
}