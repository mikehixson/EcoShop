using System.Collections.Generic;

namespace EcoShop.RecipeFilter
{
    public interface IRecipeFilter
    {
        IEnumerable<Recipe> Execute(string itemName, IEnumerable<Recipe> recipes);
    }
}