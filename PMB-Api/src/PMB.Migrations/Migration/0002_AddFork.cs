using System.Data;
using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(2)]
    public class AddFork: FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("fork")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("fork_id").AsInt64().NotNullable()
                .WithColumn("update_count").AsInt32().Nullable()
                .WithColumn("lifetime").AsInt64().NotNullable()
                .WithColumn("profit").AsDecimal().Nullable()
                .WithColumn("sport").AsString().Nullable()
                .WithColumn("bet_type").AsString().Nullable()
                .WithColumn("bookmakers").AsString().Nullable()
                .WithColumn("teams").AsString().Nullable()
                .WithColumn("other").AsString().Nullable()
                .WithColumn("created_at").AsDateTimeOffset().Nullable()
                .WithColumn("first_bet_id").AsInt64().Nullable()
                .WithColumn("second_bet_id").AsInt64().Nullable()
                .WithColumn("crid_id").AsString().Nullable()
                .WithColumn("k1").AsString().Nullable()
                .WithColumn("k2").AsString().Nullable()
                .WithColumn("el_id").AsString().Nullable();

            Create.UniqueConstraint("unique_fork_constr").OnTable("fork")
                .Columns("sport", "bet_type", "bookmakers", "crid_id");

            Create.ForeignKey("fk_first_bet_id").FromTable("fork").ForeignColumn("first_bet_id").ToTable("bet")
                .PrimaryColumn("id").OnDelete(Rule.Cascade);
            
            Create.ForeignKey("fk_second_bet_id").FromTable("fork").ForeignColumn("second_bet_id").ToTable("bet")
                .PrimaryColumn("id").OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("fork");
        }
    }
}