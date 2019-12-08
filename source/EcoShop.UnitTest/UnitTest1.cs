using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace EcoShop.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var context = new TestGameContext();

            using var stream = File.OpenRead(@"Data\Recipe.json");
            context.Recipes = await JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });


            var store = new RecipeStore(context.Recipes);


        }
}
}
