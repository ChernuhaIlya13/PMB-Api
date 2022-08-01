using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace PMB.Migrations
{
    public class Program
    {
        public static void  Main(string[] args)
        {
            if (args.Contains("--dryrun"))
            {
                return;
            }

            MigrateDatabase();
        }

        private static void MigrateDatabase()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config["ConnectionStrings:ForksDb"];
            var migrationRunner = new MigratorRunner(connectionString);
            migrationRunner.Migrate();
        }
    }
}
