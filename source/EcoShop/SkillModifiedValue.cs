using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public class SkillModifiedValue : IValue
    {
        public string Skill { get; set; }
        public decimal Value { get; set; }

        public decimal Compute(Player player)
        {
            var skillFactor = player.SkillFactor(Skill);

            return Value * skillFactor;
        }
    }
}
