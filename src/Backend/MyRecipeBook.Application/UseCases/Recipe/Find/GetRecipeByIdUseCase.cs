using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Find
{
    public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IloggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        public GetRecipeByIdUseCase(IMapper mapper, IloggedUser loggedUser, IRecipeReadOnlyRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRecipesJson> Execute(long recipeId)
        {
            var loggedUser = await _loggedUser.User();
            var recipe = await _repository.GetById(loggedUser, recipeId);
            if (recipe == null)
            {
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            }
            return _mapper.Map<ResponseRecipesJson>(recipe);
        }
    }
}
