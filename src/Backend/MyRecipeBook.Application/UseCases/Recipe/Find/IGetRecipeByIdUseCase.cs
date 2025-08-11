using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Find
{
    public interface IGetRecipeByIdUseCase
    {
        Task<ResponseRecipesJson> Execute(long recipeId);
    }
}
