using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IloggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        public UpdateUserUseCase(IloggedUser loggedUser, 
            IUserUpdateOnlyRepository repository,
            IUserReadOnlyRepository userReadOnly,
            IUnityOfWork unityOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _userReadOnlyRepository = userReadOnly;
            _unityOfWork = unityOfWork;
        }
        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.User();
            await Validate(request, loggedUser.Email);
            var user = await _repository.GetById(loggedUser.Id);
            user.Name = request.Name;
            user.Email = request.Email;
            _repository.Update(user);
            await _unityOfWork.Commit();
        }
        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();
            var result = await validator.ValidateAsync(request);
            if (!currentEmail.Equals(request.Email))
            {
               var userExist = await _userReadOnlyRepository.ExistsUserWithEmail(request.Email);
                if (userExist)
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.REPEATED_EMAIL));
            }
            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorsMessages);
            }
        }
    }
}
