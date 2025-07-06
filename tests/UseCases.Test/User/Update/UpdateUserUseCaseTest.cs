using CommonTestsUtilities.Entities.User;
using CommonTestsUtilities.Login;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;

namespace UseCases.Test.User.Update
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            //On swagger works
            (var user, var _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            var useCase = CreateUseCase(user);
            Func<Task> act = async () => await useCase.Execute(request);
            Assert.Equal(request.Name, user.Name);
            Assert.Equal(request.Email, user.Email);
        }
        [Fact]
        public async Task ErrorNameEmpty()
        {
            //On swagger works
            (var user, var _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase(user);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.Execute(request));
            Assert.Equal(ResourceMessagesException.NAME_EMPTY, exception.Message);
            Assert.NotEqual(request.Name, user.Name);
            Assert.NotEqual(request.Email, user.Email);
        }
        [Fact]
        public async Task ErrorEmailEmpty()
        {
            //On swagger works
            (var user, var _) = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Email = string.Empty;
            var useCase = CreateUseCase(user);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.Execute(request));
            Assert.Equal(ResourceMessagesException.EMAIL_EMPTY, exception.Message);
            Assert.NotEqual(request.Name, user.Name);
            Assert.NotEqual(request.Email, user.Email);
        }
        private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, string? email = null)
        {
            //On swagger works
            var unitOfWork = UnityOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if (email.NotEmpty())
                userReadOnlyRepositoryBuilder.ExistsUserWithEmail(email);
            return new UpdateUserUseCase(loggedUser, userUpdateRepository, userReadOnlyRepositoryBuilder.Build(), unitOfWork);
        }
    }
}
