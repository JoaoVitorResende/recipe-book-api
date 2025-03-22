using System.Data.Common;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DataBaseMigrations
    {
        public static void Migrate(string connectionString, IServiceProvider provider)
        {
            EnsureDataBaseCreated(connectionString);
            MigrationDataBase(provider);
        }
        private static void EnsureDataBaseCreated(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
            var dataBase = connectionStringBuilder.Database;
            connectionStringBuilder.Remove("Database");
            ConfigureDataBase(connectionStringBuilder, dataBase);
        }
        private static void ConfigureDataBase(MySqlConnectionStringBuilder connectionStringBuilder, string dataBase)
        {
            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name", dataBase);
            CreateDataBase(dbConnection, dataBase, parameters);
        }
        private static void CreateDataBase(MySqlConnection dbConnection, string dataBase, DynamicParameters parameters)
        {
            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);
            if (!records.Any())
                dbConnection.Execute($" CREATE DATABASE {dataBase}");
        }
        private static void MigrationDataBase(IServiceProvider provider)
        {
            var runner = provider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
