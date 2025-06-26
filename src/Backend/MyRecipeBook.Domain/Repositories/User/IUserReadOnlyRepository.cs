namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistsUserWithEmail(string email);
        public Task<bool> ExistsUserWithIdentifier(Guid userIdentifier);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    }
}
