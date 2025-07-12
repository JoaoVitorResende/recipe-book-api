using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace UseCases.Test.User.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            //Works on swagger
            (var user, var password) = UserBuilder.Build();
            var request = RequestChangePasswordJsonBuilder.Build();
            request.Password = password;
            var useCase = CreateUseCase(user);
            Func<Task> act = async () => await useCase.Execute(request);
            var newPasswordEncripted = PasswordEncripterBuilder.Build().Encript(request.NewPassword);
            Assert.Matches(user.Password, newPasswordEncripted);
        }
        private static ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var unitOfWork = UnityOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var encripter = PasswordEncripterBuilder.Build();
            return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, encripter);
        }
    }
}
