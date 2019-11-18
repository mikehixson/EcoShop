using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public class BasicValue : IValue
    {
        public decimal Value { get; set; }

        public decimal Compute(Player player)
        {
            return Value;
        }
    }
}
