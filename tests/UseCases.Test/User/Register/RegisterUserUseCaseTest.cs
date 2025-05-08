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
            var mapper = MapperBuilder.Build();
            var encripter = PasswordEncripterBuilder.Build();
            var WritteRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var useCase = new RegisterUserUseCase(encripter,mapper);
            var result = await useCase.Execute(request);
            Assert.NotNull(result);
            Assert.Equal(result.Name, request.Name);
        }
    }
}
