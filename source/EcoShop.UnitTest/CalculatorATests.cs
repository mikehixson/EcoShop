using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace EcoShop.UnitTest
{
    public class CalculatorATests
    {

        [Fact]
        public void Calculate_SingleStepRecipe_CorrectPartsList() 
        {
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new[] { TestRecipes.IronBar })
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Iron Bar");
                        

            var ironOre = Assert.Single(parts);
            Assert.Equal("Iron Ore", ironOre.Name);
            Assert.Equal(10, ironOre.Amount);
        }


        [Fact]
        public void Calculate_MultiStepRecipe_CorrectPartsList()
        {
            var recipe = TestRecipes.IronGearFromIronBar;
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new [] { TestRecipes.IronGearFromIronBar, TestRecipes.IronBar })
                
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Iron Gear");
                       

            Assert.Equal(2, parts.Count());

            var ironOre = Assert.Single(parts, p => p.Name == "Iron Ore");
            Assert.Equal(30, ironOre.Amount);

            var ironBar = Assert.Single(parts, p => p.Name == "Iron Bar");
            Assert.Equal(3, ironBar.Amount);
        }

        

        [Fact]
        public void Calculate_RecipeWithAdditionalProduct_AdditionalProductIsCountedAsUnused()
        {
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new[] { TestRecipes.PreparedMeat })
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Prepared Meat");


            var tailings = Assert.Single(parts.Unused);
            Assert.Equal("Scrap Meat", tailings.Name);
            Assert.Equal(2, tailings.Amount);
        }

        [Fact]
        public void Calculate_SecondStepRecipeWithAdditionalProduct_AdditionalProductIsCountedAsUnused()
        {
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new[] { TestRecipes.MeatStock, TestRecipes.PreparedMeat })
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Meat Stock");


            var preparedMeat = Assert.Single(parts.Unused);
            Assert.Equal("Prepared Meat", preparedMeat.Name);
            Assert.Equal(10, preparedMeat.Amount);
        }


        // Consuming a recipe that produces multiple of the item we need
        [Fact]
        public void Calculate_RecipeProducesMultipleOfRequiredItem_rightamounnt()   // no unused
        {
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new[] { TestRecipes.MeatStock, TestRecipes.PreparedMeat })
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Meat Stock", 1);

            var scrapMeat = Assert.Single(parts, p => p.Name == "Scrap Meat");
            Assert.Equal(20, scrapMeat.Amount);
        }



        //todo: test RecipeProducesMultipleOfRequiredItem We dont consume all. -- remainder goes to unused
        //todo: test RecipeProducesMultipleItems -- all of unused item goes to unused
        
        // Building 1 Simmered Meat
        // We need 40 (2 x 20) total Scrap Meat for the 2 Meat Stock 
        // We need 5 Prepared Meat, which also produces 10 (5 x 2) Scrap Meat
        // To ge the additional 30 Scrap Meat we should see 15 more Prepared Meat created
        [Fact]
        public void Calculate_UnusedItems_Consumed()
        {
            var context = new TestGameContext
            {
                Player = new Player(),
                Recipes = new RecipeStore(new[] { TestRecipes.SimmeredMeat, TestRecipes.PreparedMeat, TestRecipes.MeatStock })
            };

            var calculator = new CalculatorA(context);
            var parts = calculator.Calculate("Simmered Meat");

            
            // Todo: these assertions should change when we move beyond just tracking unused, to try to use the unused.
            var preparedMeat = Assert.Single(parts, p => p.Name == "Prepared Meat");
            Assert.Equal(5, preparedMeat.Amount);

            var preparedMeatUnused = Assert.Single(parts.Unused, p => p.Name == "Scrap Meat");
            Assert.Equal(10, preparedMeatUnused.Amount);


            var scrapMeat = Assert.Single(parts, p => p.Name == "Scrap Meat");
            Assert.Equal(40, scrapMeat.Amount);

            var scrapMeatUnused = Assert.Single(parts.Unused, p => p.Name == "Prepared Meat");
            Assert.Equal(20, scrapMeatUnused.Amount);
        }


    }
}
