using System;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace EcoShop
{
    class Program
    {
        private static ItemCost[] _costs;
        private static Recipe[] _recipes;

        static async Task Main(string[] args)
        {
            await Task.WhenAll(LoadCosts(), LoadRecipes());

            var player = new Player(new[]
            {
                new Skill("Basic Engineering", 7),//0.80M),
                new Skill("Mechanics", 7),//0.75M),
                new Skill("Industry", 2),//0.50M),
                new Skill("Smelting", 7),//0.80M),
                //new Skill("Advanced Smelting", 7),//0.80M)
                //new Skill("Cement", 6),
                //new Skill("Glassworking", 7)
            },
                new string[]
                {
                    "BasicEngineeringLavishResourcesTalent",
                    "MechanicsLavishResourcesTalent"
                }
            );


            player.PreferedRecipies = new[]
            {
                // Iron Gear using Iron Bar
                _recipes.Single(r => r.Products.Any(p => p.Name == "Iron Gear") && r.Ingredients.Any(i => i.Name == "Iron Bar")),

                // Steel using Coal
                _recipes.Single(r => r.Products.Any(p => p.Name == "Steel") && r.Ingredients.Any(i => i.Name == "Coal"))
            };



            var products = new[]
            {
                "Hand Plough",
                "Wooden Elevator",
                "Steam Truck",
                "Steam Tractor",
                "Mechanical Water Pump",
                "Steam Tractor Sower",
                "Steam Tractor Harvester",
                "Steam Tractor Plough",
                "Steam Engine",
                "Blast Furnace",
                "Cement Kiln",
                "Electric Machinist Table",
                "Asphalt Road",
                "Asphalt Ramp",
                "Steel",
                "Reinforced Concrete",
                "Framed Glass",
                "Waste Filter",
                "Steam Engine",
                "Electric Water Pump"
            };

            foreach (var product in products)
            {
                Console.WriteLine(product);

                var shoppingList = new Dictionary<string, decimal>();
                Calculate(player, product, shoppingList);

                var amount = 0M;
                foreach (var item in shoppingList)
                {
                    var cost = _costs.Single(c => c.Name == item.Key);

                    var itemTotal = Math.Ceiling(item.Value) * cost.Amount;
                    amount += itemTotal;

                    Console.WriteLine($"{item.Key,-20} {item.Value,7:0.00} {itemTotal,7:0.00}");
                }

                Console.WriteLine("{0,36}", $"Total: {amount,7:0.00}");
                Console.WriteLine();
            }
        }

        private static void Report(Dictionary<string, string> shoppingList)
        {

        }

        // Imorvments can be made. We cant take the .7 iron bars leftover from recipe and use them in another recipie. Thats what this assumes.
        // We should actually be able to calculate waste.
        private static void Calculate(Player player, string item, Dictionary<string, decimal> shoppingList, decimal q = 1)
        {
            var recipe = player.PreferedRecipies.SingleOrDefault(r => r.Products.Any(p => p.Name == item));
            
            if(recipe == null)
                recipe = _recipes.Single(r => r.Products.Any(p => p.Name == item)); 

            var product = recipe.Products.Single(p => p.Name == item);

            foreach (var ingredient in recipe.Ingredients)
            {
                // Sometimes a recipe creates more than 1
                var quantity = (ingredient.Quantity.Compute(player) / product.Quantity.Compute(player)) * q;

                // todo: handle the case when recipie creates 2 distinct products. Handle the case where both products are used in the main thinkg being built

                var cost = _costs.SingleOrDefault(c => c.Name == ingredient.Name);

                if (cost != null)
                {
                    if (shoppingList.TryGetValue(ingredient.Name, out var current))
                        shoppingList[ingredient.Name] = current += quantity;
                    else
                        shoppingList[ingredient.Name] = quantity;
                }
                else
                    Calculate(player, ingredient.Name, shoppingList, quantity);
            }
        }

        private static async Task LoadCosts()
        {
            using var stream = File.OpenRead("Cost.json");
            _costs = await JsonSerializer.DeserializeAsync<ItemCost[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, ReadCommentHandling = JsonCommentHandling.Skip });
        }

        private static async Task LoadRecipes()
        {
            using var stream = File.OpenRead(@"Data\Recipe.json");
            _recipes = await JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
        }
    }
}
