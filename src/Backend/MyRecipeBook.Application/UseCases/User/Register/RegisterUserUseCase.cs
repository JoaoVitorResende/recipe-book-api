using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegistredUserJson Execute(RequestRegisterUserJson response)
        {
            Validate(response);
            return new ResponseRegistredUserJson 
            {
                Name  = response.Name
            };
        }
        private void Validate(RequestRegisterUserJson response)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(response);
            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorsMessages);
            }
        }
    }
}
