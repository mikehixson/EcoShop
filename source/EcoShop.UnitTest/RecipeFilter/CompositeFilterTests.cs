using EcoShop.RecipeFilter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.RecipeFilter
{
    public class CompositeFilterTests
    {
        [Fact]
        public void Execute_AllFiltersSatisfied_Matches()
        {
            var recipes = new[] { TestRecipes.IronBar };

            var filterA = new ProductFilter();
            var filterB = new SkillFilter(new Skill("Smelting", 1));

            var filter = new CompositeFilter(filterA, filterB);

            var matches = filter.Execute("Iron Bar", recipes);

            Assert.Equal(recipes, matches);
        }

        [Fact]
        public void Execute_SomeFiltersSatisfied_NoMatches()
        {
            var recipes = new[] { TestRecipes.IronBar };

            var filterA = new ProductFilter();
            var filterB = new SkillFilter(new Skill("Smelting", 1));

            var filter = new CompositeFilter(filterA, filterB);

            var matches = filter.Execute("Copper Bar", recipes);

            Assert.Empty(matches);
        }
    }
}
