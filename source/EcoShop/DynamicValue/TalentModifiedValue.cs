using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop.DynamicValue
{
    public class TalentModifiedValue : IDynamicValue
    {
        public string Talent { get; set; }
        public decimal[] Values { get; set; }

        public decimal Compute(Player player)
        {
            if (player.Talents.Any(t => t == Talent))
                return Values[1];

            return Values[0];
        }
    }
}
