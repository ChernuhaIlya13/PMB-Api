using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(6)]
    public class TelegramStorage: FluentMigrator.Migration 
    {
        public override void Up()
        {
            const string sql = @"
                CREATE TABLE IF NOT EXISTS telegram_users (
                    telegram_chat_id bigint not null,
                    bot_key text not null,
                    is_active boolean not null,
                    bot_settings text not null   
                );
            ";
            
            Execute.Sql(sql);
        }

        public override void Down() => throw new System.NotImplementedException("Only up");
    }
}