using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository
    {
        private readonly MyrecipeBookDbContext _dbContext;
        public RecipeRepository(MyrecipeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);
    }
}
