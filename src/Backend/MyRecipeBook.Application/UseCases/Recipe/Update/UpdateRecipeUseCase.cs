using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionBase;
using MyRecipeBook.Exceptions;
using AutoMapper;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Application.UseCases.Recipe.Update
{
    public class UpdateRecipeUseCase : IUpdateRecipeUseCase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IRecipeUpdateOnlyRepository _repositoryUpdate;
        private readonly IloggedUser _loggedUser;
        private readonly IMapper _mapper;
        public UpdateRecipeUseCase(IUnityOfWork unityOfWork,
            IRecipeUpdateOnlyRepository repositoryRead,
            IMapper mapper,
        IloggedUser loggedUser)
        {
            _unityOfWork = unityOfWork;
            _repositoryUpdate = repositoryRead;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }
        public async Task Execute(long id, RequestRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            var recipe = await _repositoryUpdate.GetById(loggedUser, id) ?? throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            recipe.Ingredients.Clear();
            recipe.Instructions.Clear();
            recipe.DishTypes.Clear();
            _mapper.Map(request, recipe);
            var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
            for (var index = 0; index < instructions.Count; index++)
                instructions.ElementAt(index).Step = index + 1;
            recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);
            _repositoryUpdate.Update(recipe);
            await _unityOfWork.Commit();
        }
        private void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);
            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).Distinct().ToList());
        }
    }
}
