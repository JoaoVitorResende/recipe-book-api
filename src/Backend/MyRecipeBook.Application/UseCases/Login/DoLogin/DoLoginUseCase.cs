using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
   

    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncryption _passwordEncryption;
        public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncryption passwordEncryption)
        {
            _repository = repository;
            _passwordEncryption = passwordEncryption;
        }
        public async Task<ResponseRegistredUserJson> Execute(RequestLoginJson requestLoginJson)
        {
            var encriptedPassword = _passwordEncryption.Encript(requestLoginJson.Password);
            var user = await _repository.GetByEmailAndPassword(requestLoginJson.Email, encriptedPassword)
                ?? throw new InvalidLoginException();
            return new ResponseRegistredUserJson
            {
                Name = user.Name
            };
        }
    }
}
