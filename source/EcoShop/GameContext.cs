using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace EcoShop
{
    public class GameContext : IGameContext
    {
        public RecipeStore Recipes { get; private set; }
        public IEnumerable<ItemCost> Costs { get; private set; }

        public Player Player { get; set; }

        public GameContext()
        {

        }

        public async Task Initialize()
        {
            await Task.WhenAll(LoadCosts(), LoadRecipes());
        }

        private async Task LoadRecipes()
        {
            using var stream = File.OpenRead(@"Data\Recipe.json");
            var recipes = await JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });

            Recipes = new RecipeStore(recipes);
        }

        private async Task LoadCosts()
        {
            using var stream = File.OpenRead("Cost.json");

            Costs = await JsonSerializer.DeserializeAsync<ItemCost[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, ReadCommentHandling = JsonCommentHandling.Skip });
        }
    }    
}
