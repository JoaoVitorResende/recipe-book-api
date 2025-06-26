using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Tokens;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = password
            });
            Assert.NotNull(result);
            Assert.NotNull(result.Name);
            Assert.NotNull(result.Tokens);
            Assert.NotEqual("", result.Name);
            Assert.NotEmpty(result.Tokens.AccessToken);
            Assert.NotNull(result.Tokens.AccessToken);
        }
        [Fact]
        public async Task ErrorInvalidUser()
        {
            var request = RequestLoginJsonBuilder.Build();
            var useCase = CreateUseCase();
            Func<Task> act = async() => { await useCase.Execute(request); };
            var error = await Assert.ThrowsAsync<InvalidLoginException>(act);
            Assert.Equal(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID, error.Message);
        }
        private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.build();
            if (user is not null)
                userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);
            return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(),  passwordEncripter, accessTokenGenerator);
        }
    }
}
