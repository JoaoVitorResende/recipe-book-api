using MyRecipeBook.Domain.Repositories.UnityOfWork;

namespace MyRecipeBook.Infrastructure.DataAccess.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly MyrecipeBookDbContext _dbContext;
        public UnityOfWork(MyrecipeBookDbContext dbContext) => _dbContext = dbContext;
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
