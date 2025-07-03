using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Exceptions;

namespace Validator.Test.User.Update
{
    public class UpdateProfileValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ErrorNameEmpty()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.NAME_EMPTY);
        }
        [Fact]
        public void ErrorEmailEmpty()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Email = string.Empty;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.EMAIL_EMPTY);
        }
        [Fact]
        public void ErrorEmailInvalid()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Email = "teste";
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.EMAIL_WRONG_FORMAT);
        }
    }
}
