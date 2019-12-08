using EcoShop.RecipeFilter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.RecipeFilter
{
    public class SkillFilterTests
    {
        [Fact]
        public void Execute_ValidSkill_Matches()
        {
            var recipes = new[] { TestRecipes.IronBar };
            var skills = new[] { new Skill("Smelting", 1) };

            var filter = new SkillFilter(skills);
            var matches = filter.Execute("Iron Bar", recipes);

            Assert.Equal(recipes, matches);
        }

        [Fact]
        public void Execute_InvalidSkillByName_NoMatches()
        {
            var recipes = new[] { TestRecipes.IronBar };
            var skills = new[] { new Skill("Advanced Smelting", 1) };

            var filter = new SkillFilter(skills);
            var matches = filter.Execute("Iron Bar", recipes);

            Assert.Empty(matches);
        }

        [Fact]
        public void Execute_InvalidSkillByLevel_NoMatches()
        {
            var recipes = new[] { TestRecipes.Brazier };
            var skills = new[] { new Skill("Smelting", 0) };

            var filter = new SkillFilter(skills);
            var matches = filter.Execute("Brazier", recipes);

            Assert.Empty(matches);
        }
    }
}
