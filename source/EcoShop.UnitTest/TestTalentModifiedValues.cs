using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.UnitTest
{
    public static class TestTalentModifiedValues
    {
        public static TalentModifiedValue SmeltingLavishResourcesTalent = new TalentModifiedValue
        { 
            Talent = "SmeltingLavishResourcesTalent",
            Values = new[] { 1.0M, 0.9M }
        };
    }
}
