using System.Net;
using System.Text.Json;
using CommonTestsUtilities.Tokens;

namespace WebApi.Test.Recipe.Find
{
    public class GetRecipeByIdTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe";

        private readonly Guid _userIdentifier;
        private readonly string _recipeId;
        private readonly string _recipeTitle;
        public GetRecipeByIdTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _recipeId = factory.GetRecipeId();
            _recipeTitle = factory.GetRecipeTitle();
        }
        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoGet($"{METHOD}/{_recipeId}", token);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            using var responseData = JsonDocument.Parse(responseBody);
            var root = responseData.RootElement;
            Assert.Equal(_recipeId, root.GetProperty("id").GetString());
            Assert.Equal(_recipeTitle, root.GetProperty("title").GetString());
        }
    }
}
