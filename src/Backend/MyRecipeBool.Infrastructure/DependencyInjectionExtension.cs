using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.DataAccess.UnityOfWork;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBook.Infrastructure.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Validator;
using MyRecipeBook.Infrastructure.Services.LoggedUser;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordsEncrpter(services, configuration);
            AddRepositories(services);
            AddLoggedUser(services);
            AddTokens(services,configuration);
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
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
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
        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }
        private static void AddLoggedUser(IServiceCollection services)
        {
            services.AddScoped<IloggedUser, LoggedUser>();
        }
        private static void AddPasswordsEncrpter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Passowords:AdditionalKey");
            services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
        }
    }
}
