using System.Diagnostics.CodeAnalysis;
using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Communication.Enuns;
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
        [Fact]
        public void Success_DishTypes_Empty()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.DishTypes.Clear();
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void Error_Invalid_DishTypes()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.DishTypes.Add((DishType)1000);
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.DISH_TYPE_NOT_SUPPORTED);
        }
        [Fact]
        public void Error_Empty_Ingredients()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Ingredients.Clear();
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.AT_LEAST_ONE_INGREDIENT);
        }
        [Fact]
        public void Error_Empty_Instructions()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Instructions.Clear();
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.AT_LEAST_ONE_INSTRUCTION);
        }
        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Because it is a unit test")]
        public void Error_Empty_Value_Ingredients(string ingredient)
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Ingredients.Add(ingredient);
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.INGREDIENT_EMPTY);
        }
        [Fact]
        public void Error_Same_Step_Instructions()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Instructions.First().Step = request.Instructions.Last().Step;
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.TWO_OR_MORE_INSTRUCTIONS_SAME_ORDER);
        }
        [Fact]
        public void Error_Negative_Step_Instructions()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Instructions.First().Step = -1;
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.NON_NEGATIVE_INSTRUCTION_STEP);
        }
        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Because it is a unit test")]
        public void Error_Empty_Value_Instructions(string instruction)
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Instructions.First().Text = instruction;
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.INSTRUCTION_EMPTY);
        }
        [Fact]
        public void Error_Instructions_Too_Long()
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Instructions.First().Text = RequestStringGenerator.Paragraphs(minCharacters: 2001);
            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].ErrorMessage, ResourceMessagesException.INSTRUCTION_EXCEEDS_LIMIT_CHARACTERS);
        }
    }
}
