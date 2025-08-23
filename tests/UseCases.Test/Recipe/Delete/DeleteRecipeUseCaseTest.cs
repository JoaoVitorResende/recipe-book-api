using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Repositories;
using CommonTestUtilities.Entities;
using MyRecipeBook.Application.UseCases.Recipe.Delete;

namespace UseCases.Test.Recipe.Delete
{
    public class DeleteRecipeUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);
            var useCase = DeleteUseCase(user, recipe);
            var exception = await Record.ExceptionAsync(() => useCase.Execute(recipe.Id));
            Assert.Null(exception); // Passa se nenhuma exceção for lançada
        }

        private static DeleteRecipeUseCase DeleteUseCase(MyRecipeBook.Domain.Entities.User user, MyRecipeBook.Domain.Entities.Recipe recipe)
        {
            var unitOfWork = UnityOfWorkBuilder.Build();
            var repositoryWrite = RecipeWriteOnlyRepositoryBuilder.Build();
            var repositoryRead = new RecipeReadOnlyRepositoryBuilder().GetById(user, recipe).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            return new DeleteRecipeUseCase(unitOfWork, repositoryWrite, repositoryRead,loggedUser);
        }
    }
}
