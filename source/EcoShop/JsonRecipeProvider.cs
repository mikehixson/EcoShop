using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcoShop
{
    public class JsonRecipeProvider : IRecipeProvider
    {
        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            using (var stream = File.OpenRead(@"Data\Recipe.json"))
            {
                return await JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
            }
        }
    }
}
