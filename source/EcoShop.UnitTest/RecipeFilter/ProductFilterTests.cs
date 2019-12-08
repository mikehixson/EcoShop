using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using EcoShop.RecipeFilter;

namespace EcoShop.UnitTest.RecipeFilter
{
    public class ProductFilterTests
    {
        [Fact]
        public void Execute_ValidName_Matches()
        {
            var recipes = new[] { TestRecipes.IronBar };

            var filter = new ProductFilter();
            var matches = filter.Execute("iron bar", recipes);

            Assert.Equal(recipes, matches);
        }

        [Fact]
        public void Execute_InvalidName_NoMatches()
        {
            var recipes = new[] { TestRecipes.IronGearFromIronBar };

            var filter = new ProductFilter();
            var matches = filter.Execute("iron bar", recipes);

            Assert.Empty(matches);
        }
    }
}
