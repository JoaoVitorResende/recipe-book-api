using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly IloggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        public FilterRecipeUseCase(IMapper mapper, IRecipeReadOnlyRepository repository, IloggedUser loggedUser)
        {
            _mapper = mapper;
            _repository = repository;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            var filters = new Domain.Dtos.FilterRecipesDto
            {
                RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
                CookingTimes = request.CookingTimes.Distinct().Select(c => (Domain.Enum.CookingTime)c).ToList(),
                Difficulties = request.Difficulties.Distinct().Select(c => (Domain.Enum.Difficulty)c).ToList(),
                DishTypes = request.DishTypes.Distinct().Select(c => (Domain.Enum.DishType)c).ToList()
            };
            var recipes = await _repository.Filter(loggedUser, filters);
            return new ResponseRecipesJson
            {
                Recipes = _mapper.Map<List<ResponseShortRecipeJson>>(recipes)
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
