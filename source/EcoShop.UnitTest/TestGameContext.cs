using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EcoShop.UnitTest
{
    public class TestGameContext : IGameContext
    {
        public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<ItemCost> Costs { get; set; }

        RecipeStore IGameContext.Recipes => throw new NotImplementedException();

        public Player Player => throw new NotImplementedException();

        public TestGameContext()
        {
            using var stream = File.OpenRead(@"Data\Recipe.json");
            Recipes = JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip }).Result;
        }
    }
}
