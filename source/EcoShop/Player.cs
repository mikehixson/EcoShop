using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop
{
    public class Player
    {
        public Skill[] Skills { get; }

        public Player(Skill[] skills)
        {
            Skills = skills;
        }

        public decimal SkillFactor(string skillName)
        {
            var skill = Skills.SingleOrDefault(s => s.Name == skillName);

            if (skill == null)
                return 1;

            return 1 - skill.Benefit;
        }
    }
}
