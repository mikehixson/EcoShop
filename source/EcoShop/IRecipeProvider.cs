using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop
{
    public interface IRecipeProvider
    {
        Task<IEnumerable<Recipe>> GetRecipes();
    }
}
