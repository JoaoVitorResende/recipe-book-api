﻿using MyRecipeBook.Communication.Enuns;

namespace MyRecipeBook.Communication.Requests
{
    public class RequestRecipeJson
    {
        public string Title {  get; set; } = string.Empty;
        public CookingTime? Cookingtime {  get; set; } 
        public Difficulty? Difficulty {  get; set; }
        public IList<string> Ingredients { get; set; } = [];
        public IList<RequestInstructionJson> Instructions { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
    }
}
