using System.Collections.Generic;
using System.Linq;

namespace EcoShop.RecipeFilter
{
    public class SkillFilter : IRecipeFilter
    {
        private readonly IEnumerable<Skill> _skills;

        public SkillFilter(params Skill[] skills)
        {
            _skills = skills;
        }

        public SkillFilter(IEnumerable<Skill> skills)
        {
            _skills = skills;
        }

        public IEnumerable<Recipe> Execute(string itemName, IEnumerable<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                if (!recipe.Skills.Any())
                    yield return recipe;


                if (recipe.Skills.Join(_skills, recipeSkill => recipeSkill.Name, specSkill => specSkill.Name, (recipeSkill, specSkill) => recipeSkill.Level <= specSkill.Level).Any(m => m))
                    yield return recipe;
            }
        }
    }

}