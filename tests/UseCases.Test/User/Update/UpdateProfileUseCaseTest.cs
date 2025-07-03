using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Update;

namespace UseCases.Test.User.Update
{
    public class UpdateProfileUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            //var useCase = CreateUseCase(user);
            //Func<Task> act = async () => await useCase.Execute(request);

        }
        /*private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var userUpadateRepository = new UserUpdateOnlyRepositoryBuilder().
            var loggedUser = LoggedUserBuilder.Build(user);
            return new UpdateUserUseCase();
         }*/
    }
}
