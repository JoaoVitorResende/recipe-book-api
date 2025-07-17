using Bogus;
using MyRecipeBook.Communication.Enuns;
using MyRecipeBook.Communication.Requests;

namespace CommonTestsUtilities.Requests
{
    public class RequestRecipeJsonBuilder
    {
        public static RequestRecipeJson Build()
        {
            return new Faker<RequestRecipeJson>()
            .RuleFor(recipe => recipe.Title, (faker) => faker.Lorem.Word())
            .RuleFor(recipe => recipe.Cookingtime, (faker) => faker.PickRandom<CookingTime>())
            .RuleFor(recipe => recipe.Difficulty, (faker) => faker.PickRandom<Difficulty>());
        }
    }
}
