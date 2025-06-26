using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly MyrecipeBookDbContext _dbContext;
        public UserRepository(MyrecipeBookDbContext dbContext) => _dbContext = dbContext;
        public async Task Add(User user) => await _dbContext.AddAsync(user);
        public async Task<bool> ExistsUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        public async Task<bool> ExistsUserWithIdentifier(Guid userIdentifier) 
        {
            var user = await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
            return user;
        }
        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password)
                 && user.Active);
        }
    }
}
