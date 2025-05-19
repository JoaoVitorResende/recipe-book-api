using System.Net.Http;
using System.Net.Http.Json;
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
        public async Task Succes()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            await _httpClient.PostAsJsonAsync("User", request);
        }
    }
}
