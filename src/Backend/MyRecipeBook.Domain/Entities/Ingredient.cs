using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipeBook.Domain.Entities
{
    [Table("Ingredients")]
    public class Ingredient
    {
        public string Item { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
