using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IPasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository readOnlyRepository, IMapper mapper, IPasswordEncripter passwordEncripter, IUnityOfWork unityOfWork, IAccessTokenGenerator accessTokenGenerator)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unityOfWork = unityOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }
        public async Task<ResponseRegistredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);
            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encript(request.Password);
            user.UserIdentifier = Guid.NewGuid();
            await _writeOnlyRepository.Add(user);
            await _unityOfWork.Commit();
            return new ResponseRegistredUserJson
            {
                Name = request.Name,
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            var emailExists = await _readOnlyRepository.ExistsUserWithEmail(request.Email);
            if(emailExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,ResourceMessagesException.REPEATED_EMAIL));
            }
            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorsMessages);
            }
        }
    }
}
