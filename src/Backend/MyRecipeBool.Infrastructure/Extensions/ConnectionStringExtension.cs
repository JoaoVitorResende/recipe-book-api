using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions
{
    public static class ConnectionStringExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
        public static string GetConnectionStringExtension(this IConfiguration configuration) => configuration.GetConnectionString("Connection")!;
    }
}
