using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions
{
    public static class ConnectionStringExtension
    {
        public static string GetConnectionStringExtension(this IConfiguration configuration) => configuration.GetConnectionString("Connection")!;
    }
}
