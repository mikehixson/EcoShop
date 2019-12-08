using EcoShop.RecipeFilter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.RecipeFilter
{
    public class PreferredFilterTests
    {
        [Fact]
        public void Execute_ItemNameMatchRecipeInstanceMatch_Matches()
        {
            var recipes = new[] { TestRecipes.IronGearFromIronBar };

            var filter = new PreferredFilter();
            filter.Add("iron gear", TestRecipes.IronGearFromIronBar);

            var matches = filter.Execute("iron gear", recipes);

            Assert.Equal(recipes, matches);
        }

        [Fact]
        public void Execute_ItemNameMatchRecipeInstanceNoMatch_NoMatches()
        {
            var recipes = new[] { TestRecipes.IronGearFromSteel };

            var filter = new PreferredFilter();
            filter.Add("iron gear", TestRecipes.IronGearFromIronBar);

            var matches = filter.Execute("iron gear", recipes);

            Assert.Empty(matches);
        }

        [Fact]
        public void Execute_ItemNameNoMatch_Matches()
        {
            var recipes = new[] { TestRecipes.IronBar };

            var filter = new PreferredFilter();
            filter.Add("iron gear", TestRecipes.IronGearFromIronBar);

            var matches = filter.Execute("iron bar", recipes);

            Assert.Equal(recipes, matches);
        }
    }
}
