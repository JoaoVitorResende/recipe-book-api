using System.Data.Common;
using Dapper;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DataBaseMigrations
    {
        public static void Migrate(string connectionString)
        {
            EnsureDataBaseCreated(connectionString);
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
    }
}
