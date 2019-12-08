using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoShop.RecipeFilter
{
    public class PreferredFilter : IRecipeFilter
    {
        private readonly Dictionary<string, Recipe> _index;

        public PreferredFilter()
        {
            _index = new Dictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(string itemName, Recipe recipe)
        {
            _index.Add(itemName, recipe);
        }

        public IEnumerable<Recipe> Execute(string itemName, IEnumerable<Recipe> recipes)
        {
            if (!_index.TryGetValue(itemName, out var recipe))
                return recipes;

            if (recipes.Contains(recipe))
                return new[] { recipe };

            return Enumerable.Empty<Recipe>();
        }
    }
}