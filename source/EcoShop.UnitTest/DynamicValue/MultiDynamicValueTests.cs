using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.DynamicValue
{
    public class MultiDynamicValueTests
    {
        [Fact]
        public void Compute_PlayerWithSkillAndTalent_ReturnsProduct()
        {
            var player = new Player()
            {
                Skills = { new Skill("Smelting", 1) },
                Talents = { "SmeltingLavishResourcesTalent" },
            };
            
            var dynamicValue = TestMultiDynamicValue.SmeltingAndSmeltingLavish;
            var value = dynamicValue.Compute(player);

            var expected = ((SkillModifiedValue)dynamicValue.Children[0]).Values[1] * ((TalentModifiedValue)dynamicValue.Children[1]).Values[1];

            Assert.Equal(expected, value);
        }

        [Fact]
        public void Compute_PlayerWithSkillLevel1_ReturnsValueIndex1()
        {
            var player = new Player()
            {
                Skills = { new Skill("Smelting", 1) },
            };

            var dynamicValue = TestMultiDynamicValue.SmeltingAndSmeltingLavish;
            var value = dynamicValue.Compute(player);

            var expected = ((SkillModifiedValue)dynamicValue.Children[0]).Values[1];

            Assert.Equal(expected, value);
        }
    }
}
