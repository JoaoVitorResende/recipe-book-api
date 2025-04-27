using System.ComponentModel.DataAnnotations;
using Bogus;
using MyRecipeBook.Communication.Requests;
namespace CommonTestsUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (faker) => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.Name, (faker) => faker.Internet.Password());
        }
    }
}
