using System.Collections.Generic;

namespace EcoShop.RecipeFilter
{
    public class CompositeFilter : IRecipeFilter
    {
        private readonly IEnumerable<IRecipeFilter> _filters;

        public CompositeFilter(params IRecipeFilter[] filters)
        {
            _filters = filters;
        }

        public CompositeFilter(IEnumerable<IRecipeFilter> filters)
        {
            _filters = filters;
        }

        public IEnumerable<Recipe> Execute(string itemName, IEnumerable<Recipe> recipes)
        {
            foreach (var filter in _filters)
                recipes = filter.Execute(itemName, recipes);

            return recipes;
        }
    }

}