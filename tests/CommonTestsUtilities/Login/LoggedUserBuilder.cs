using Moq;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace CommonTestsUtilities.Login
{
    public class LoggedUserBuilder
    {
        public static IloggedUser Build(MyRecipeBook.Domain.Entities.User user)
        {
            var mock = new Mock<IloggedUser>();
            mock.Setup(x => x.User()).ReturnsAsync(user);
            return mock.Object;
        }
    }
}
