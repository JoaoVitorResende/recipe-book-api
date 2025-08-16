using System.Net;
using CommonTestsUtilities.IdEncripter;
using CommonTestsUtilities.Tokens;

namespace WebApi.Test.Recipe.Find
{
    public class GetRecipeByIdInvalidTokenTest : MyRecipeBookClassFixture
    {
        private const string method ="recipe";
        public GetRecipeByIdInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }
        [Fact]
        public async Task ErrorTokenInvalid()
        {
            var id = IdEncripterBuilder.Build().Encode(1);
            var response = await DoGet($"{method}/{id}", token: "tokenInvalid");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task ErrorTokenWithUserNotFound()
        {
            var id = IdEncripterBuilder.Build().Encode(1);
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
            var response = await DoGet($"{method}/{id}", token: token);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
