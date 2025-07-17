using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Exceptions;

namespace Validator.Test.Recipe
{
    public class RecipeValidatorTest
    {
        [Fact]
        public void Succes()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ErrorInvalidCookingTime()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            request.Cookingtime = (MyRecipeBook.Communication.Enuns.CookingTime?)4;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void ErrorInvalidDifficulty()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            request.Difficulty = (MyRecipeBook.Communication.Enuns.Difficulty?)4;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void ErrorInvalidTtile()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            request.Title = string.Empty;
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.RECIPE_TITLE_EMPTY);
        }
        [Fact]
        public void SuccessWhenCookingTimeIsNull()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            request.Cookingtime = null;
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void SuccessWhenDifficultyIsNull()
        {
            var validator = new RecipeValidator();
            var request = RequestRecipeJsonBuilder.Build();
            request.Difficulty = null;
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
    }
}
