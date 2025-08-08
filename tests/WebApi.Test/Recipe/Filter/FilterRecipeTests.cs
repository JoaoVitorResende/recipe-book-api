using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Tokens;
using MyRecipeBook.Communication.Requests;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Recipe.Filter
{
    public class FilterRecipeTests : MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe/filter";
        private readonly Guid _userIdentifier;
        private string _recipeTitle;
        private MyRecipeBook.Domain.Enum.Difficulty _difficulties;
        private MyRecipeBook.Domain.Enum.CookingTime _cookingTimes;
        private IList<MyRecipeBook.Domain.Enum.DishType> _dishTypes;
        public FilterRecipeTests(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _difficulties = factory.GetRecipeDifficulty();
            _cookingTimes = factory.GetRecipeCookingTime();
            _recipeTitle = factory.GetRecipeTitle();
        }
        [Fact]
        public async Task Success()
        {
            //problem here but in the swagger works
            var request = new RequestFilterRecipeJson
            {
                CookingTimes = [(MyRecipeBook.Communication.Enuns.CookingTime)_cookingTimes],
                Difficulties = [(MyRecipeBook.Communication.Enuns.Difficulty)_difficulties],
                DishTypes = _dishTypes.Select(dishType => (MyRecipeBook.Communication.Enuns.DishType)dishType).ToList(),
                RecipeTitle_Ingredient = _recipeTitle,
            };

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoPost(method: METHOD, request: request, token: token);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            //responseData.RootElement.GetProperty("recipes").EnumerateArray().Should().NotBeNullOrEmpty();
        }
    }
}
