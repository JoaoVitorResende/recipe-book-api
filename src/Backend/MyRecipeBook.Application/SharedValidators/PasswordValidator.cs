﻿using FluentValidation;
using FluentValidation.Validators;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.SharedValidators
{
    public class PasswordValidator<T>: PropertyValidator<T,string>
    {
        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage",ResourceMessagesException.PASSWORD_EMPTY);
                return false;
            }
            if (password.Length < 6)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_NOT_GREAT_THEN_SIX_CHARACTERS);
                return false;
            }
            return true;
        }
        public override string Name => "PasswordValidator";
        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
       
    }
    
}
