using Bogus;
using CommonTestsUtilities.Cryptography;
namespace CommonTestsUtilities.Entities.User
{
    public class UserBuilder
    {
        public static (MyRecipeBook.Domain.Entities.User user, string password) Build()
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var password = new Faker().Internet.Password();
            var user = new Faker<MyRecipeBook.Domain.Entities.User>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid())
                .RuleFor(user => user.Password, (f) => passwordEncripter.Encript(password));
            return (user, password);
        }
    }
}
