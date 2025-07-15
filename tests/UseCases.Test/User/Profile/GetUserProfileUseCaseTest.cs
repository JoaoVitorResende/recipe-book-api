using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using MyRecipeBook.Application.UseCases.User.Profile;

namespace UseCases.Test.User.Profile
{
    public class GetUserProfileUseCaseTest
    {
        [Fact]
        public async Task Succes()
        {
            (var user, var _) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            var result = await useCase.Execute();
            Assert.NotNull(result);
            Assert.Equal(result.Name, user.Name);
            Assert.Equal(result.Email, user.Email);
        }
        private static GetUserProfileUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            return new GetUserProfileUseCase(loggedUser, mapper);
        }
    }
}
