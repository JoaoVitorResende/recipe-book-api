﻿using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_NOT_GREAT_THEN_SIX_CHARACTERS);
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_WRONG_FORMAT);
            });
        }
    }
}
