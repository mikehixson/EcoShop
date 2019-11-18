using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public interface IValue
    {
        decimal Compute(Player player);
    }
}
