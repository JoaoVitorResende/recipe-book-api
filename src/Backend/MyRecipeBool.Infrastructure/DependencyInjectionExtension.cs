using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.DataAccess.UnityOfWork;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddRepositories(services);
            AddDbContext(services);
            SaveRepositories(services);
        }
        private static void AddDbContext(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database=myrecipebook;Uid=root;Pwd=root;";
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
    }
}
