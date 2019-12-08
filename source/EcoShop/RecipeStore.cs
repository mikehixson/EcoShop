using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using EcoShop.RecipeFilter;

namespace EcoShop
{
    public class RecipeStore
    {
        private readonly IEnumerable<Recipe> _recipes;

        public RecipeStore(IEnumerable<Recipe> recipes)
        {
            _recipes = recipes;
        }

        public IEnumerable<Recipe> GetRecipes(string itemName, IRecipeFilter filter)
        {
            return filter.Execute(itemName, _recipes);
        }

        public IEnumerable<Recipe> GetRecipes(Func<Recipe, bool> predicate)
        {
            return _recipes.Where(predicate);
        }
    }

}