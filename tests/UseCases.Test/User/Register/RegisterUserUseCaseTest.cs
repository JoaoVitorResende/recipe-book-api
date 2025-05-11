using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;

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
        }
        [Fact]
        public async Task ErrorEmailAlreadyRegistred()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase(request.Email);
        }
        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var encripter = PasswordEncripterBuilder.Build();
            var WriteRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if(!string.IsNullOrEmpty(email))
                readRepositoryBuilder.ExistsUserWithEmail(email);
            return new RegisterUserUseCase(WriteRepository, readRepositoryBuilder.Build(), mapper, encripter, unityOfWork);
        }
    }
}
