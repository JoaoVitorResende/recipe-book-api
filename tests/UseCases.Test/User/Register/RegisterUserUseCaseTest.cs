using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Tokens;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);
            Assert.NotNull(result);
            Assert.Equal(result.Name, request.Name);
            Assert.NotNull(result.Tokens);
            Assert.NotEmpty(result.Tokens.AccessToken);
            Assert.NotNull(result.Tokens.AccessToken);
        }
        [Fact]
        public async Task ErrorEmailAlreadyRegistred()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase(request.Email);
            Func<Task> exception = async() => await useCase.Execute(request);
            await Assert.ThrowsAsync<ErrorOnValidationException>(exception);
            var exceptionOnValidation = await Assert.ThrowsAsync<ErrorOnValidationException>(() => useCase.Execute(request));
            var errorCount = exceptionOnValidation.ErrorMessages.Count;
            Assert.Equal(1, errorCount);
            Assert.Equal(ResourceMessagesException.REPEATED_EMAIL, exceptionOnValidation.ErrorMessages[0]);
        }
        [Fact]
        public async Task ErrorNameIsEmpty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase();
            Func<Task> exception = async () => await useCase.Execute(request);
            await Assert.ThrowsAsync<ErrorOnValidationException>(exception);
            var exceptionOnValidation = await Assert.ThrowsAsync<ErrorOnValidationException>(() => useCase.Execute(request));
            var errorCount = exceptionOnValidation.ErrorMessages.Count;
            Assert.Equal(1, errorCount);
            Assert.Equal(ResourceMessagesException.NAME_EMPTY, exceptionOnValidation.ErrorMessages[0]);
        }
        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var encripter = PasswordEncripterBuilder.Build();
            var WriteRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.build();
            if (!string.IsNullOrEmpty(email))
                readRepositoryBuilder.ExistsUserWithEmail(email);
            return new RegisterUserUseCase(WriteRepository, readRepositoryBuilder.Build(), mapper, encripter, unityOfWork, accessTokenGenerator);
        }
    }
}
