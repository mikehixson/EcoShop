using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.DynamicValue
{
    public class ConstantValue : IDynamicValue
    {
        public decimal Value { get; set; }

        public decimal Compute(Player player)
        {
            return Value;
        }
    }
}
