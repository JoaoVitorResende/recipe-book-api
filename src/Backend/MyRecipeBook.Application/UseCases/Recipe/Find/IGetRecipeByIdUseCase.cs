using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Find
{
    public interface IGetRecipeByIdUseCase
    {
        Task<ResponseRecipeJson> Execute(long recipeId);
    }
}
