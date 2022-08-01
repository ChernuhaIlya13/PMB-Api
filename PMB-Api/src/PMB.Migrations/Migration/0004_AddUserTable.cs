using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(4)]
    public class AddUserTable: FluentMigrator.Migration 
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("login").AsString().PrimaryKey()
                .WithColumn("email").AsString().Unique().NotNullable()
                .WithColumn("password_hash").AsString().NotNullable();

            Create.Table("user_key")
                .WithColumn("login").AsString().NotNullable()
                .WithColumn("key").AsString().NotNullable()
                .WithColumn("key_expiration_time").AsDateTimeOffset().NotNullable()
                .WithColumn("freeze_time").AsDateTimeOffset().Nullable();
        }
        
        public override void Down()
        {
            throw new System.NotImplementedException("Only up");
        }
    }
}