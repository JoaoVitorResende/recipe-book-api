using Moq;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestsUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;
        public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();
        public IUserReadOnlyRepository Build() => _repository.Object;
        public void ExistsUserWithEmail(string email)
        {
            _repository.Setup(repository => repository.ExistsUserWithEmail(email)).ReturnsAsync(true);
        }
        public void GetByEmailAndPassword(User user) => _repository.Setup(repository => repository.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
    }
}
