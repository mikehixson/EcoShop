using EcoShop.RecipeFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoShop
{
    public class CalculatorA
    {
        private readonly IGameContext _gameContext;

        public CalculatorA(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public PartsList CalculateTemp(string item, decimal quantity = 1)
        {
            var preferred = new PreferredFilter();

            // Iron Gear using Iron Bar
            preferred.Add("Iron Gear", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Iron Gear") && r.Ingredients.Any(i => i.Name == "Iron Bar")).Single());

            // Steel using Coal
            preferred.Add("Steel", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Steel") && r.Ingredients.Any(i => i.Name == "Coal")).Single());

            // Raw Meat using Bison Carcass
            preferred.Add("Raw Meat", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Raw Meat") && r.Ingredients.Any(i => i.Name == "Bison Carcass")).Single());

            // Scrap Meat using Raw Meat
            preferred.Add("Scrap Meat", _gameContext.Recipes.GetRecipes(r => r.Products.All(p => p.Name == "Scrap Meat") && r.Ingredients.Any(i => i.Name == "Raw Meat")).Single());

            // Sugar
            preferred.Add("Sugar", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Sugar")).First());

            // Vegetable Medley            
            preferred.Add("Vegetable Medley", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Vegetable Medley")).First());

            // Fruit Salad
            preferred.Add("Fruit Salad", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Fruit Salad")).First());

            // Basic Salad
            preferred.Add("Basic Salad", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Basic Salad")).First());

            // Tallow
            preferred.Add("Tallow", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Tallow")).First());

            // Leather Hide
            preferred.Add("Leather Hide", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Leather Hide")).First());

            // Fur Pelt
            preferred.Add("Fur Pelt", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Fur Pelt")).First());


            var filter = new CompositeFilter
                (
                    new ProductFilter(),
                    preferred
                );


            var partsList = new PartsList();

            Calculate(item, partsList, filter, quantity);

            return partsList;
        }

        public PartsList Calculate(string item, decimal quantity = 1)
        {

            var partsList = new PartsList();

            Calculate(item, partsList, new ProductFilter(), quantity);

            return partsList;
        }


        // Improvments can be made. We cant take the .7 iron bars leftover from recipe and use them in another recipie. Thats what this assumes.
        // We should actually be able to calculate waste.
        // Handle the case where both products are used in the main thinkg being built
        private void Calculate(string item, PartsList shoppingList, IRecipeFilter filter, decimal q = 1)
        {
            var recipes = _gameContext.Recipes.GetRecipes(item, filter);

            if (!recipes.Any())
                return; // Item cant be crafted with a recipe.. Iron Ore etc..

            if (recipes.Count() > 1)
                throw new Exception($"Multiple recipes were found that produce {item}.");

            var recipe = recipes.Single();
            var principal = recipe.Products.Single(p => p.Name == item);


            // q = how many we need, count = how many times this recipe must be executed.
            var count = q / principal.Quantity.Compute(_gameContext.Player);


            foreach (var product in recipe.Products)
            {
                // This recipe has additional products, all will be considered unused
                if (product != principal)
                {
                    shoppingList.IncrementUnused(product.Name, product.Quantity.Compute(_gameContext.Player) * count);
                    continue;
                }

                // Portion of principal that will be unused
                var unused = q - (count * principal.Quantity.Compute(_gameContext.Player));

                if (unused > 0)
                    shoppingList.IncrementUnused(product.Name, unused);


                foreach (var ingredient in recipe.Ingredients)
                {
                    var quantity = ingredient.Quantity.Compute(_gameContext.Player) * count;
                    
                    shoppingList.Increment(ingredient.Name, quantity);
                    Calculate(ingredient.Name, shoppingList, filter, quantity);
                }
            }
        }
    }
}
