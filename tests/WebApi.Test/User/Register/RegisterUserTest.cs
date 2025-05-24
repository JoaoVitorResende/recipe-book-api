using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Test.User.Register
{
   
    public class RegisterUserTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }
       [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
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
