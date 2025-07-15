namespace MyRecipeBook.Domain.Entities
{
    public class DishType: EntitieBase
    {
        public Enum.DishType Type{ get; set; }
        public long RecipeId { get; set; }
    }
}
