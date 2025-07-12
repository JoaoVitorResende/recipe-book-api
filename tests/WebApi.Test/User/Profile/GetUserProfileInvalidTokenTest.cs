using System.Net;
using CommonTestsUtilities.Tokens;

namespace WebApi.Test.User.Profile
{
    public class GetUserProfileInvalidTokenTest: MyRecipeBookClassFixture
    {
        private readonly string METHOD = "user";
        public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
            
        }
        [Fact]
        public async Task ErrorTokenInvalid()
        {
            var response = await DoGet(METHOD, token:"tokenInvalid");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task ErrorWithoutToken()
        {
            var response = await DoGet(METHOD, token: string.Empty);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task ErrorWithUserNotFound()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
            var response = await DoGet(METHOD, token: token);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
