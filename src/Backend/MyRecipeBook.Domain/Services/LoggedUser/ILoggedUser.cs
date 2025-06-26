using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Services.LoggedUser
{
    public interface IloggedUser
    {
        public Task<User> User();
    }
}
