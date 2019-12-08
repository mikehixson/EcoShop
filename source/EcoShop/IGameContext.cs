using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public interface IGameContext
    {
        RecipeStore Recipes { get; }
        IEnumerable<ItemCost> Costs { get; }

        Player Player { get; }
    }
}
