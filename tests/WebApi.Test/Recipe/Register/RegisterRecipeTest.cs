using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Tokens;
using MyRecipeBook.Exceptions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Recipe.Register;
public class RegisterRecipeTest : MyRecipeBookClassFixture
{
    private const string METHOD = "recipe";

    private readonly Guid _userIdentifier;

    public RegisterRecipeTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRecipeJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token);

        //response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        //responseData.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        //responseData.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = string.Empty;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: METHOD, request: request, token: token, culture: culture);

       Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("RECIPE_TITLE_EMPTY", new CultureInfo(culture));
        Assert.Equivalent(1, errors.Count());
        Assert.Equivalent(ResourceMessagesException.RECIPE_TITLE_EMPTY, expectedMessage);
    }
}