﻿using AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegistredUserJson Execute(RequestRegisterUserJson request)
        {
            var passwordEncripter = new PasswordEncryption();
            Validate(request);
            var user = AutoMapUser(request).Map<Domain.Entities.User>(request);
            user.Password = passwordEncripter.Encript(request.Password);
            return new ResponseRegistredUserJson 
            {
                Name  = request.Name
            };
        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorsMessages);
            }
        }
        private IMapper AutoMapUser(RequestRegisterUserJson request) => new AutoMapper.MapperConfiguration(options => { options.AddProfile(new AutoMapping()); }).CreateMapper();
    }
}
