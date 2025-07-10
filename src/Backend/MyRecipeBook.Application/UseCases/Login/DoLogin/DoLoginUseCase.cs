using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncryption;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncryption, IAccessTokenGenerator accessTokenGenerator)
        {
            _repository = repository;
            _passwordEncryption = passwordEncryption;
            _accessTokenGenerator = accessTokenGenerator;
        }
        public async Task<ResponseRegistredUserJson> Execute(RequestLoginJson requestLoginJson)
        {
            var encriptedPassword = _passwordEncryption.Encript(requestLoginJson.Password);
            var user = await _repository.GetByEmailAndPassword(requestLoginJson.Email, encriptedPassword)
                ?? throw new InvalidLoginException();
            return new ResponseRegistredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
    }
}
