using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(5)]
    public class AddUserRoles: FluentMigrator.Migration
    {
        public override void Up()
        {
            const string sql = @"
                create table if not exists user_roles(
	                login text references users(login),
	                role text
                )";

            Execute.Sql(sql);
        }

        public override void Down()
        {
            throw new System.NotImplementedException("Only up");
        }
    }
}