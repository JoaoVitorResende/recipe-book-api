using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Filter;

namespace Validator.Test.Recipe.Filter
{
    public class FilterRecipeValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new FilterValidator();
            var request = RequestFilterRecipeJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
    }
}
