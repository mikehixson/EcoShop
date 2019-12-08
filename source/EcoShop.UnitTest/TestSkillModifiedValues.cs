using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.UnitTest
{
    public static class TestSkillModifiedValues
    {
        public static SkillModifiedValue Smelting = new SkillModifiedValue()
        { 
            Skill = "Smelting",
            Values = new[] { 8.0M, 7.0M, 6.0M, 5.0M, 4.0M, 3.0M, 2.0M, 1.0M }
        };
    }
}
