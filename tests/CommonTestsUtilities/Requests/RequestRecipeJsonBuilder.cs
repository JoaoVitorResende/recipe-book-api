using Bogus;
using MyRecipeBook.Communication.Enuns;
using MyRecipeBook.Communication.Requests;

namespace CommonTestsUtilities.Requests
{
    public class RequestRecipeJsonBuilder
    {
        public static RequestRecipeJson Build()
        {
            var step = 1;
            return new Faker<RequestRecipeJson>()
            .RuleFor(recipe => recipe.Title, (faker) => faker.Lorem.Word())
            .RuleFor(recipe => recipe.Cookingtime, (faker) => faker.PickRandom<CookingTime>())
            .RuleFor(recipe => recipe.Difficulty, (faker) => faker.PickRandom<Difficulty>())
            .RuleFor(recipe => recipe.Ingredients, (faker) => faker.Make(3, () => faker.Commerce.ProductName()))
            .RuleFor(recipe => recipe.DishTypes, (faker) => faker.Make(3, () => faker.PickRandom<DishType>()))
            .RuleFor(recipe => recipe.Instructions, (faker) => faker.Make(3, () => new RequestInstructionJson
            { 
                Text = faker.Lorem.Paragraph(),
                Step = step++,
            }));
        }
    }
}
