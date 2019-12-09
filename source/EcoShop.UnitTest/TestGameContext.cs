using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EcoShop.UnitTest
{
    public class TestGameContext : IGameContext
    {
        //public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<ItemCost> Costs { get; set; }

        public RecipeStore Recipes { get; set; }

        public Player Player { get; set; }

        public TestGameContext()
        {
            Costs = new List<ItemCost>();
        }
    }
}
