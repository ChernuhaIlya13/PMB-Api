using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace PMB.Migrations
{
    public class MigratorRunner
    {
        private readonly string _connectionString;

        public MigratorRunner(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Migrate()
        {
            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(serviceProvider.GetRequiredService<IMigrationRunner>());
        }   

        private IServiceProvider CreateServices()
        {
            Console.WriteLine(typeof(MigratorRunner).Assembly.FullName);
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithVersionTable(new VersionTable())
                    .WithGlobalConnectionString(_connectionString)
                    .ScanIn(typeof(MigratorRunner).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private void UpdateDatabase(IMigrationRunner runner)
        {
            runner.MigrateUp();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            connection.ReloadTypes();
        }
    }
}