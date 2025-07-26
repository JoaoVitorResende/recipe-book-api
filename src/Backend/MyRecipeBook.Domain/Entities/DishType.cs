using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipeBook.Domain.Entities
{
    [Table("DishTypes")]
    public class DishType: EntitieBase
    {
        public Enum.DishType Type{ get; set; }
        public long RecipeId { get; set; }
    }
}
