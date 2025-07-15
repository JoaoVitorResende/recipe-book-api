namespace MyRecipeBook.Domain.Entities
{
    public class Ingredient
    {
        public string Item { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
