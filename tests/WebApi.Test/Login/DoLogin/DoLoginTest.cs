using CommonTestsUtilities.Requests;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using MyRecipeBook.Communication.Requests;

namespace WebApi.Test.Login.DoLogin
{
    public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly string _method = "login";
        private readonly HttpClient _httpClient;
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;
        public DoLoginTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _name = factory.GetName();
        }
        [Fact]
        public async Task Succes()
        {
            //middleware brake this code but in browser it works..
            var request = new RequestLoginJson
            { 
                Email = _email,
                Password = _password
            };
            var response = await _httpClient.PostAsJsonAsync(_method, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);
            Assert.Equal(responseData.RootElement.GetProperty("name").GetString(), _name);
        }
    }
}
