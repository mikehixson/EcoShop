using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.UnitTest
{
    public static class TestMultiDynamicValue
    {
        public static MultiDynamicValue SmeltingAndSmeltingLavish = new MultiDynamicValue
        {
            Children =
            {
                TestSkillModifiedValues.Smelting,
                TestTalentModifiedValues.SmeltingLavishResourcesTalent
            }
        };
    }
}
