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
    public class TelegramUserRepository: PmbRepository
    {
        public TelegramUserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<TelegramUserDal> Insert(TelegramUserDal model, CancellationToken token)
        {
            if (model.TelegramChatId <= 0 || string.IsNullOrEmpty(model.BotKey))
            {
                throw new NoDbParametersException();
            }
            
            var sql = @"
            insert into telegram_users 
            values (@TelegramChatId, @BotKey, @IsAcive, @BotSettings)
            returning 
                telegram_chat_id,
                bot_key,
                is_active,
                bot_settings
                ";
            
            var c = await GetConnAsync();
            return await c.QueryFirstAsync<TelegramUserDal>(new CommandDefinition(sql, new
            {
                model.TelegramChatId,
                model.BotKey,
                model.IsActive,
                BotSettings = model.BotSettings ?? "''"
            }, cancellationToken: token));
        }

        public async Task<TelegramUserDal[]> GetTelegramUsersQuery(GetTelegramUsersQueryModel query, CancellationToken token)
        {
            var sql = @"
                select * from telegram_users
            ";

            var param = new DynamicParameters();
            var conditions = new List<string>();

            if (query.IsActive.HasValue)
            {
                param.Add("IsActive", query.IsActive.Value);
                conditions.Add("is_active = @IsActive");
            }

            if (query.BotKeys?.Any() == true)
            {
                param.Add("BotKeys", query.BotKeys);
                conditions.Add("bot_key = ANY(@BotKeys)");
            }

            if (conditions.Count == 0)
            {
                throw new NoDbParametersException();
            }

            var c = await GetConnAsync();

            return (await c.QueryAsync<TelegramUserDal>(new CommandDefinition(sql, param, cancellationToken: token)))
                .ToArray();
        }
    }
}