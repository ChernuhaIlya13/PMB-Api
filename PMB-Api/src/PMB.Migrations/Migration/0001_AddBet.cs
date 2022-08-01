using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(1)]
    public class AddTables : FluentMigrator.Migration
    {
        public override void Up()
        {
           Create.Table("bet")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("bookmaker").AsString().NotNullable()
                .WithColumn("coefficient").AsDecimal().NotNullable()
                .WithColumn("direction").AsString().NotNullable()
                .WithColumn("bet_type").AsString().NotNullable()
                .WithColumn("bet_id").AsString().Nullable()
                .WithColumn("sport").AsString().NotNullable()
                .WithColumn("parameter").AsDecimal().NotNullable()
                .WithColumn("bet_value").AsString().Nullable()
                .WithColumn("forks_count").AsInt32().NotNullable()
                .WithColumn("ev_id").AsString().Nullable()
                .WithColumn("other_data").AsString().Nullable()
                .WithColumn("teams").AsString().Nullable()
                .WithColumn("match_data").AsString().Nullable()
                .WithColumn("url").AsString().Nullable()
                .WithColumn("is_req").AsBoolean().NotNullable()
                .WithColumn("is_initiator").AsBoolean().NotNullable();
        }
        
        public override void Down()
        {
            Delete.Table("bet");
        }
    }
}