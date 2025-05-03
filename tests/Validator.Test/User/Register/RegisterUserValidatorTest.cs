using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;

namespace Validator.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ErrorName()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal(ResourceMessagesException.NAME_EMPTY, result.Errors.First().ToString());
        }
        [Fact]
        public void ErrorEmail()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal(ResourceMessagesException.EMAIL_EMPTY, result.Errors.First().ToString());
        }
    }
}
