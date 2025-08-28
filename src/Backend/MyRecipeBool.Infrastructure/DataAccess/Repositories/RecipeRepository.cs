using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
    {
        private readonly MyrecipeBookDbContext _dbContext;
        public RecipeRepository(MyrecipeBookDbContext dbContext) => _dbContext = dbContext;
        public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);
        public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
        {
            var query = _dbContext.Recipes.AsNoTracking().Include(recipe => recipe.Ingredients).Where(recipe => recipe.Active && recipe.UserId == user.Id);
            if(filters.Difficulties.Any())
            {
                query = query.Where(recipe => recipe.Difficulty.HasValue && filters.Difficulties.Contains(recipe.Difficulty.Value));
            }
            if (filters.CookingTimes.Any())
            {
                query = query = query.Where(recipe => recipe.CookingTime.HasValue && filters.CookingTimes.Contains(recipe.CookingTime.Value));
            }

            if (filters.DishTypes.Any())
            {
                query = query = query.Where(recipe => recipe.DishTypes.Any(dishType => filters.DishTypes.Contains(dishType.Type)));
            }
            if (filters.RecipeTitle_Ingredient.NotEmpty())
            {
                query = query.Where(
                    recipe => recipe.Title.Contains(filters.RecipeTitle_Ingredient)
                    || recipe.Ingredients.Any(ingredient => ingredient.Item.Contains(filters.RecipeTitle_Ingredient)));
            }
            return await query.ToListAsync();
        }
        async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .AsNoTracking()
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }
        public async Task Delete(long recipeId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(recipeId);
            _dbContext.Recipes.Remove(recipe!);
        }
        async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
              .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }
        private IIncludableQueryable<Recipe, IList<DishType>> GetFullRecipe()
        {
            return _dbContext
              .Recipes
              .Include(recipe => recipe.Ingredients)
              .Include(recipe => recipe.Instructions)
              .Include(recipe => recipe.DishTypes);
        }
        public async Task<IList<Recipe>> GetForDashboard(User user)
        {
            return await _dbContext
                .Recipes
                .AsNoTracking()
                .Include(ingredients => ingredients.Ingredients)
                .Where(recipe => recipe.Active && recipe.UserId == user.Id)
                .OrderByDescending(recipe => recipe.CreatedOn)
                .Take(2)
                .ToListAsync();
        }
        public void Update(Recipe recipe) => _dbContext.Recipes.Update(recipe);
    }
}
