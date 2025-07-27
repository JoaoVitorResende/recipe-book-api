using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace UseCases.Test.Recipe.Register
{
    public class RecipeRegisterUseCase
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestRecipeJsonBuilder.Build();
            var useCase = CreateUseCase(user);
            var result = useCase.Execute(request);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Error_Title_Empty()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestRecipeJsonBuilder.Build();
            request.Title = string.Empty;
            var useCase = CreateUseCase(user);
            var exceptionOnValidation = await Assert.ThrowsAsync<ErrorOnValidationException>(() => useCase.Execute(request));
            Assert.Equal(ResourceMessagesException.RECIPE_TITLE_EMPTY, exceptionOnValidation.ErrorMessages[0]);
        }
        private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var unityOfWork = UnityOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var recipeWriteOnly = RecipeWriteOnlyRepositoryBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            return new RegisterRecipeUseCase(recipeWriteOnly, loggedUser, unityOfWork, mapper);
        }
    }
}
