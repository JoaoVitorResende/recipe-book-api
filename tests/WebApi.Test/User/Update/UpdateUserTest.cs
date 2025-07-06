using System.Net;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Tokens;

namespace WebApi.Test.User.Update
{
    public class UpdateUserTest : MyRecipeBookClassFixture
    {
        private const string method = "user";
        private readonly Guid _userIdentifier;  
        public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }
        [Fact]

        public async Task Success()
        {
            //problem here but in browser it works..
            var request = RequestUpdateUserJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.build().Generate(Guid.NewGuid());
            var response = await DoPut(method, request, token);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
