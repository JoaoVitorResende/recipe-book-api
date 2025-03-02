namespace MyRecipeBook.Domain.Repositories.UnityOfWork
{
    public interface IUnityOfWork
    {
        public Task Commit();
    }
}
