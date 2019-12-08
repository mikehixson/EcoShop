using System;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using EcoShop.DynamicValue;
using EcoShop.RecipeFilter;

namespace EcoShop
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var gameContext = new GameContext();
            await gameContext.Initialize();

            gameContext.Player = new Player
            {
                Skills =
                {
                    new Skill("Butchery", 7),
                    new Skill("Cooking", 3),
                    //new Skill("Mechanics", 7),
                    //new Skill("Industry", 4),
                    //new Skill("Electronics", 7),
                    //new Skill("Oil Drilling", 7),
                },
                Talents =
                {
                    "ButcheryLavishResourcesTalent",
                    //"BasicEngineeringLavishResourcesTalent",
                    //"MechanicsLavishResourcesTalent"
                }
            };


            var calculator = new CalculatorA(gameContext);
            //var pl = calculator.Calculate("Robotic Assembly Line");


            // Find all recipes where skills provide a benefit
            var products = gameContext.Recipes.GetRecipes(r => true)
                .Where(r => r.Ingredients.Any(i => new string[] { "Butchery", "Cooking" }.Contains(Skm(i.Quantity)?.Skill)))
                .SelectMany(r => r.Products)
                .Select(p => p.Name)
                .Distinct()
                .Where(p => gameContext.Costs.All(c => c.Name != p));


            SkillModifiedValue Skm(IDynamicValue value)
            {
                if (value is SkillModifiedValue smv)
                    return smv;

                if (value is MultiDynamicValue mdv)
                {
                    foreach (var v in mdv.Children)
                    {
                        var r = Skm(v);

                        if (r != null)
                            return r;
                    }
                }

                return null;
            }


            var products2 = new[]
            {
                "Fruit Salad",
                "Scrap Meat",
                "Prepared Meat",
                "Prime Cut",
                "Raw Bacon",
                "Raw Roast",
                "Raw Sausage",
                "Simmered Meat",               


                //"Hand Plough",
                //"Wooden Elevator",
                //"Steam Truck",
                //"Steam Tractor",
                //"Mechanical Water Pump",
                //"Steam Tractor Sower",
                //"Steam Tractor Harvester",
                //"Steam Tractor Plough",
                //"Steam Engine",
                //"Blast Furnace",
                //"Cement Kiln",
                //"Electric Machinist Table",
                //"Asphalt Road",
                //"Asphalt Ramp",
                //"Steel",
                //"Reinforced Concrete",
                //"Framed Glass",
                //"Waste Filter",
                //"Steam Engine",
                //"Electric Water Pump",
                //"Combustion Engine",
                //"Oil Refinery",
                //"Pump Jack",
                //"Electronics Assembly",
                //"Servo",
                //"Circuit",
                //"Copper Wiring",
                //"Robotic Assembly Line",
                //"Substrate",
                //"Fiberglass",
                //"Petroleum",
                //"Electric Wall Lamp",
                //"Combustion Generator",
                //"Biodiesel",
                //"Sink",
                //"Excavator",
                //"Wind Turbine",
                //"Streetlamp",
                //"Steel Ceiling Light",
                //"Steel Floor Lamp",
                //"Steel Table Lamp",
                //"Truck",
                //"Steel Plate",
                //"Computer Lab",
                //"Laser"
            };

            int count = 1;

            foreach (var product in products)
            {
                Console.WriteLine(product);

                var partsList = calculator.Calculate(product, count);
                Report(gameContext, partsList);

                Console.WriteLine();
            }
        }

        private static void Report(IGameContext context, PartsList partsList)
        {
            var amount = 0M;
            foreach (var item in partsList)
            {
                var cost = context.Costs.Single(c => c.Name == item.Name);

                var itemTotal = Math.Ceiling(item.Amount) * cost.Amount;
                amount += itemTotal;

                Console.WriteLine($"{item.Name,-20} {item.Amount,7:0.00} {itemTotal,7:0.00}");
            }

            Console.WriteLine("{0,36}", $"Total: {amount,7:0.00}");

        }
    }


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

    public class CalculatorA
    {
        private readonly IGameContext _gameContext;

        public CalculatorA(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        // The specification cant just be "Is this Recipe valid". It must be "Is this recipe valid to make x"

        public PartsList Calculate(string item, decimal quantity = 1)
        {
            var preferred = new PreferredFilter();

            // Iron Gear using Iron Bar
            preferred.Add("Iron Gear", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Iron Gear") && r.Ingredients.Any(i => i.Name == "Iron Bar")).Single());

            // Steel using Coal
            preferred.Add("Steel", _gameContext.Recipes.GetRecipes(r => r.Products.Any(p => p.Name == "Steel") && r.Ingredients.Any(i => i.Name == "Coal")).Single());

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

        // Imorvments can be made. We cant take the .7 iron bars leftover from recipe and use them in another recipie. Thats what this assumes.
        // We should actually be able to calculate waste.
        // It looks like Arrows are an example of less effency with more levels.
        private void Calculate(string item, PartsList shoppingList, IRecipeFilter filter, decimal q = 1)
        {
            //var recipe = _gameContext.Recipes.GetRecipe(new CompositeSpecification(new[] { recipeSpecification, new ItemNameSpecification(item, r => r.Products) }));
            var recipes = _gameContext.Recipes.GetRecipes(item, filter);

            if (!recipes.Any())
                return; // Item cant be crafted with a recipe.. Iron Ore etc..

            if (recipes.Count() > 1)
                throw new Exception($"Multiple recipes were found that produce {item}.");

            var recipe = recipes.Single();
            var product = recipe.Products.Single(p => p.Name == item);

            foreach (var ingredient in recipe.Ingredients)
            {
                // Sometimes a recipe creates more than 1
                var quantity = (ingredient.Quantity.Compute(_gameContext.Player) / product.Quantity.Compute(_gameContext.Player)) * q;

                // todo: handle the case when recipe creates 2 distinct products. Handle the case where both products are used in the main thinkg being built

                var cost = _gameContext.Costs.SingleOrDefault(c => c.Name == ingredient.Name);

                if (cost != null)
                {
                    shoppingList.Increment(ingredient.Name, quantity);
                }
                else
                {
                    Calculate(ingredient.Name, shoppingList, filter, quantity);
                }
            }
        }
    }

    public class PartsList : IEnumerable<PartsListItem>
    {
        private readonly Dictionary<string, PartsListItem> _index;

        public PartsList()
        {
            _index = new Dictionary<string, PartsListItem>();
        }

        public void Increment(string name, decimal amount)
        {
            if (!_index.TryGetValue(name, out var item))
            {
                item = new PartsListItem(name);
                _index.Add(name, item);
            }

            item.Increment(amount);
        }

        public void Decrement(string name, decimal amount)
        {
            if (!_index.TryGetValue(name, out var item))
            {
                item = new PartsListItem(name);
                _index.Add(name, item);
            }

            item.Decrement(amount);
        }

        public IEnumerator<PartsListItem> GetEnumerator()
        {
            return _index.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PartsListItem
    {
        public string Name { get; }
        public decimal Amount { get; private set; }

        public PartsListItem(string name)
        {
            Name = name;
        }

        public void Increment(decimal amount)
        {
            Amount += amount;
        }

        public void Decrement(decimal amount)
        {
            Amount -= amount;
        }
    }
}
