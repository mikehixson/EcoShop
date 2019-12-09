using System;
using System.Linq;
using System.Threading.Tasks;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(product);
                Console.ResetColor();

                var partsList = calculator.CalculateTemp(product, count);
                Report(gameContext, partsList);
                Console.WriteLine();

                ReportUnused(partsList);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void Report(IGameContext context, PartsList partsList)
        {
            var amount = 0M;
            foreach (var item in partsList)
            {
                var cost = context.Costs.SingleOrDefault(c => c.Name == item.Name) ?? new ItemCost { Amount = 0 };

                var itemTotal = Math.Ceiling(item.Amount) * cost.Amount;
                amount += itemTotal;

                Console.WriteLine($"{item.Name,-20} {item.Amount,7:0.00} {itemTotal,7:0.00}");
            }

            Console.WriteLine("{0,36}", $"Total: {amount,7:0.00}");

        }

        private static void ReportUnused(PartsList partsList)
        {
            foreach (var item in partsList.Unused)
            {
                Console.WriteLine($"{item.Name,-20} {item.Amount,7:0.00}");
            }
        }
    }
}
