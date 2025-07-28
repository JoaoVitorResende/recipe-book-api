using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly IloggedUser _loggedUser;
        public FilterRecipeUseCase(IMapper mapper, IloggedUser loggedUser)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            return new ResponseRecipesJson 
            {
                Recipes = []
            };
        }
        private static void Validate(RequestFilterRecipeJson request)
        {
            var validator = new FilterValidator();
            var result = validator.Validate(request);
            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).Distinct().ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
