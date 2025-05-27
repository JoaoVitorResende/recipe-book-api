using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilities.Requests;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register
{

    public class RegisterUserTest: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }
        [Theory]
        [ClassData(typeof(CultureInlineData))]
        public async Task Success(string culture)
        {
            //middleware brake this code but in browser it works..
            var request = RequestRegisterUserJsonBuilder.Build();
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
            var response = await _httpClient.PostAsJsonAsync("User", request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            Assert.NotNull(responseData.RootElement.GetProperty("name").GetString());
            Assert.NotEmpty(responseData.RootElement.GetProperty("name").GetString()!);
            Assert.Equal(request.Name,responseData.RootElement.GetProperty("name").GetString());
        }
    }
}
