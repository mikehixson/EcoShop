using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public class Skill
    {
        public string Name { get; }
        public decimal Benefit { get; }

        public Skill(string name, decimal benefit)
        {
            Name = name;
            Benefit = benefit;
        }
    }
}
