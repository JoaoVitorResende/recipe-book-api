using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestUtilities.Entities;
using MyRecipeBook.Application.UseCases.Recipe.Update;

namespace UseCases.Test.Recipe.Update
{
    public class UpdateRecipeUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);
            var request = RequestRecipeJsonBuilder.Build();
            var useCase = CreateUseCase(user, recipe);
            var exception = await Record.ExceptionAsync(() => useCase.Execute(recipe.Id, request));
            Assert.Null(exception);
        }
        private static UpdateRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, MyRecipeBook.Domain.Entities.Recipe recipe)
        {
            var unitOfWork = UnityOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repositoryUpdate = new RecipeUpdateOnlyRepositoryBuilder().GetById(user, recipe).Build();
            return new UpdateRecipeUseCase(unitOfWork, repositoryUpdate, mapper, loggedUser);
        }
    }
}
