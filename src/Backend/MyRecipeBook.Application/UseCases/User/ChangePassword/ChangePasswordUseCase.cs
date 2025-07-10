using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IloggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IPasswordEncripter _passwordEncripter;
        public ChangePasswordUseCase(IloggedUser loggedUser,
            IUserUpdateOnlyRepository repository,
            IUnityOfWork unityOfWork,
            IPasswordEncripter passwordEncripter)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unityOfWork = unityOfWork;
            _passwordEncripter = passwordEncripter;
        }
        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.User();
            Validate(request, loggedUser);
            var user = await _repository.GetById(loggedUser.Id);
            user.Password = _passwordEncripter.Encript(request.NewPassword);
            _repository.Update(user);
            await _unityOfWork.Commit();
        }
        private void Validate(RequestChangePasswordJson request, MyRecipeBook.Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);
            var currentPasswordEncripted = _passwordEncripter.Encript(request.Password);
            if (currentPasswordEncripted.Equals(loggedUser.Password).IsFalse())
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
            if(result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}
