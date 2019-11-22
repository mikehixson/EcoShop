using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop
{
    public class SkillModifiedValue : IValue
    {
        public string Skill { get; set; }
        public decimal[] Values { get; set; }

        public decimal Compute(Player player)
        {
            var skill = player.Skills.SingleOrDefault(s => s.Name == Skill);

            if (skill == null)
                return Values[0];

            return Values[skill.Level];
        }
    }
}
