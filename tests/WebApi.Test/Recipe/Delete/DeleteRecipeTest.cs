using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestsUtilities.IdEncripter;
using CommonTestsUtilities.Tokens;
using MyRecipeBook.Exceptions;

namespace WebApi.Test.Recipe.Delete;
public class DeleteRecipeTest : MyRecipeBookClassFixture
{
    private const string METHOD = "recipe";
    private readonly Guid _userIdentifier;
    private readonly string _recipeId;
    // on swagger works 
    public DeleteRecipeTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _recipeId = factory.GetRecipeId();
    }
    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var response = await DoDelete($"{METHOD}/{_recipeId}", token);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        response = await DoGet($"{METHOD}/{_recipeId}", token);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Theory]
    [InlineData("en")]
    [InlineData("pt-BR")]
    public async Task Error_Recipe_Not_Found(string culture)
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var id = IdEncripterBuilder.Build().Encode(1000);
        var response = await DoDelete($"{METHOD}/{id}", token, culture);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray().ToList();
        var expectedMessage = ResourceMessagesException
            .ResourceManager
            .GetString("RECIPE_NOT_FOUND", new CultureInfo(culture));
        Assert.Single(errors);
        Assert.Equal(expectedMessage, errors[0].GetString());
    }
}