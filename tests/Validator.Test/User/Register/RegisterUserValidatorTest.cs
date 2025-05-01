﻿using MyRecipeBook.Application.UseCases.User.Register;
using CommonTestsUtilities.Requests;

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
    }
}
