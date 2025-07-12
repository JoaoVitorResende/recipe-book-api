using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Exceptions;

namespace Validator.Test.User.ChangePassword
{
    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ErrorPasswordInvalid(int password)
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordJsonBuilder.Build(password);
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage,ResourceMessagesException.PASSWORD_INVALID);
        }
        [Theory]
        [InlineData(0)]
        public void ErrorPasswordEmpty(int password)
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordJsonBuilder.Build(password);
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.PASSWORD_EMPTY);
        }
    }
}
