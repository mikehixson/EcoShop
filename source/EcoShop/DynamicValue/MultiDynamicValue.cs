using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop.DynamicValue
{
    public class MultiDynamicValue : IDynamicValue
    {
        public string Operation { get; set; }   //todo: validate this is always "Multiply"
        public List<IDynamicValue> Children { get; set; }

        public MultiDynamicValue()
        {
            Children = new List<IDynamicValue>();
        }

        public decimal Compute(Player player)
        {
            return Children.Select(c => c.Compute(player)).Aggregate((a, v) => a * v);
        }
    }
}
