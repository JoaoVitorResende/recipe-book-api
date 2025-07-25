﻿using MyRecipeBook.Domain.Enum;

namespace MyRecipeBook.Domain.Entities
{
    public class Recipe
    {
        public string Title { get; set; } = string.Empty;
        public CookingTime ? CookingTime {  get; set; }
        public Difficulty Difficulty { get; set; }
        public IList<Ingredient> Ingredients { get; set; } = [];
        public IList<Instruction> Instructions { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
        public long UserId { get; set; }

    }
}
