using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop
{
    public class MultiDynamicValue : IValue
    {
        public string Operation { get; set; }
        public IValue[] Children { get; set; }

        public decimal Compute(Player player)
        {
            return Children.Select(c => c.Compute(player)).Aggregate((a, v) => a * v);
        }
    }
}
