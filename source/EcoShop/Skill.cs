using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public class Skill
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public Skill(string name, int level)
        {
            Name = name;
            Level = level;  // todo: validate range?
        }

        public Skill()
        {

        }
    }
}
