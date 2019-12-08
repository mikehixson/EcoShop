using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoShop.RecipeFilter
{
    public class ProductFilter : IRecipeFilter
    {
        public IEnumerable<Recipe> Execute(string itemName, IEnumerable<Recipe> recipes)
        {
            return recipes.Where(r => r.Products.Any(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)));
        }
    }

}