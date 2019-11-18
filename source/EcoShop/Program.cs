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

            var player = new Player(new[] { new Skill("Mechanics", 0.70M) });

            var products = new[]
            {
                "Wooden Elevator",
                "Steam Truck",
                "Steam Tractor",
                "Mechanical Water Pump",
                "Steam Tractor Sower",
                "Steam Tractor Harvester",
                "Steam Tractor Plough",
                "Steam Engine"
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
        
        private static void Calculate(Player player, string item, Dictionary<string, decimal> shoppingList, decimal q = 1)
        {
            var recipe = _recipes.Single(r => r.Products.Any(p => p.Name == item)); // todo: handle the case when multipe recipes match
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
            using var stream = File.OpenRead("Recipe.json");
            _recipes = await JsonSerializer.DeserializeAsync<Recipe[]>(stream, new JsonSerializerOptions { AllowTrailingCommas = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, ReadCommentHandling = JsonCommentHandling.Skip });
        }
    }
}
