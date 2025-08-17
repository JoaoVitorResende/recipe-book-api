using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.UnityOfWork;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete
{
    public class DeleteRecipeUseCase : IDeleteRecipeUseCase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IRecipeReadOnlyRepository _repositoryRead;
        private readonly IRecipeWriteOnlyRepository _repositoryWrite;
        private readonly IloggedUser _loggedUser;
        public DeleteRecipeUseCase(IUnityOfWork unityOfWork,
            IRecipeWriteOnlyRepository repositoryWrite,
            IRecipeReadOnlyRepository repositoryRead,
            IloggedUser loggedUser)
        {
            _unityOfWork = unityOfWork;
            _repositoryWrite = repositoryWrite;
            _repositoryRead = repositoryRead;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();
            var recipe = await _repositoryRead.GetById(loggedUser, id) ?? throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            await _repositoryWrite.Delete(id);
            await _unityOfWork.Commit();
        }
    }
}
