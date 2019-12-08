using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.DynamicValue
{
    public interface IDynamicValue
    {
        decimal Compute(Player player);
    }
}
