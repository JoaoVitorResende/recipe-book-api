using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestUtilities.Entities;
using MyRecipeBook.Application.UseCases.Recipe.Filter;

namespace UseCases.Test.Recipe.Filter
{
    public class FilterRecipeUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var recipes = RecipeBuilder.Collection(user);
            var useCase = CreateUseCase(user, recipes);
            var request = RequestFilterRecipeJsonBuilder.Build();
            var result = useCase.Execute(request);
            Assert.NotNull(result);
            Assert.NotEmpty(recipes);
        }
        private static FilterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user,recipes).Build();
            return new FilterRecipeUseCase(mapper, repository, loggedUser);
        }
    }
}
