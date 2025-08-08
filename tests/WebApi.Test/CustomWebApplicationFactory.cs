using CommonTestsUtilities.Entities.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Domain.Enum;
using CommonTestUtilities.Entities;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        private MyRecipeBook.Domain.Entities.User _user = default!;
        private MyRecipeBook.Domain.Entities.Recipe _recipe = default!;
        private string _password = string.Empty; 
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test").ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault
                (d => d.ServiceType == typeof(DbContextOptions<MyrecipeBookDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<MyrecipeBookDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyrecipeBookDbContext>();
                dbContext.Database.EnsureDeleted();
                StartDataBase(dbContext);
            });
        }
        private void StartDataBase(MyrecipeBookDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();
            _recipe = RecipeBuilder.Build(_user);
            dbContext.Users.Add(_user);
            dbContext.Recipes.Add(_recipe);
            dbContext.SaveChanges();
        }
        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;
        public string GetRecipeTitle() => _recipe.Title;
        public Difficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
        public CookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
        public Guid GetUserIdentifier() => _user.UserIdentifier;
    }
}
