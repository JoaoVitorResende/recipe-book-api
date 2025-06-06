﻿using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.DataAccess.UnityOfWork;
using MyRecipeBook.Infrastructure.Extensions;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            if (configuration.IsUnitTestEnviroment())
                return;
            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
            SaveRepositories(services);
        }
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = ConnectionStringExtension.GetConnectionStringExtension(configuration);
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 41));
            services.AddDbContext<MyrecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
        private static void SaveRepositories(IServiceCollection services) => services.AddScoped<IUnityOfWork, UnityOfWork>();
        private static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = ConnectionStringExtension.GetConnectionStringExtension(configuration);
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options.AddMySql5().
                WithGlobalConnectionString(connectionString).
                ScanIn(Assembly.Load("MyRecipeBook.Infrastructure"))
                .For.All();
            });
        }
    }
}
