using System.Net;
using System.Text.Json;
using CommonTestsUtilities.Requests;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register
{

    public class RegisterUserTest: MyRecipeBookClassFixture
    {
        public RegisterUserTest(CustomWebApplicationFactory factory): base(factory) { }
        [Theory]
        [ClassData(typeof(CultureInlineData))]
        public async Task Success(string culture)
        {
            //middleware brake this code but in browser it works..
            var request = RequestRegisterUserJsonBuilder.Build();
            var response = await DoPost("User", request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            Assert.NotNull(responseData.RootElement.GetProperty("name").GetString());
            Assert.NotEmpty(responseData.RootElement.GetProperty("name").GetString()!);
            Assert.Equal(request.Name,responseData.RootElement.GetProperty("name").GetString());
        }
    }
}
