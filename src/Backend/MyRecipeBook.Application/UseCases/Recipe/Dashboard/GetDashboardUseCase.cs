﻿
using AutoMapper;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace MyRecipeBook.Application.UseCases.Recipe.Dashboard
{
    public class GetDashboardUseCase : IGetDashboardUseCase
    {
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IloggedUser _loggedUser;
        public GetDashboardUseCase(IRecipeReadOnlyRepository repository, IMapper mapper, IloggedUser loggedUser)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRecipesJson> Execute()
        {
            var loggedUser = await _loggedUser.User();
            var recipe = await _repository.GetForDashboard(loggedUser);
            return new ResponseRecipesJson
            { 
                Recipes = _mapper.Map<IList<ResponseShortRecipeJson>>(recipe)
            };

        }
    }
}
