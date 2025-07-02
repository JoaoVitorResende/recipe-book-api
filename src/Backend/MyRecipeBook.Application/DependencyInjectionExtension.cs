using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordsEncrpter(services, configuration);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
        private static void AddPasswordsEncrpter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Passowords:AdditionalKey");
            services.AddScoped(option => new PasswordEncryption(additionalKey!)); 
        }
        private static void AddUseCases(IServiceCollection services)
        { 
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>(); 
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>(); 
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>(); 
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>(); 
        }
    }
}
