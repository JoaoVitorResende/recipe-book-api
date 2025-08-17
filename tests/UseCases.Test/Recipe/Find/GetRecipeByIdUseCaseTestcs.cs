using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestUtilities.Entities;
using MyRecipeBook.Application.UseCases.Recipe.Find;
using MyRecipeBook.Domain.Entities;

namespace UseCases.Test.Recipe.Find
{
    public class GetRecipeByIdUseCaseTestcs
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);
            var useCase = CreateUseCase(user, recipe);
            var result = await useCase.Execute(recipe.Id);
            Assert.NotNull(result);
            Assert.Equal(recipe.Title, result.Title);
        }
        private static GetRecipeByIdUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, MyRecipeBook.Domain.Entities.Recipe recipe)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new RecipeReadOnlyRepositoryBuilder().GetById(user, recipe).Build();
            return new GetRecipeByIdUseCase(mapper, loggedUser, repository);
        }
    }
}
