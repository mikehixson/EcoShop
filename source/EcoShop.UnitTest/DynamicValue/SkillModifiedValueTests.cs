using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.DynamicValue
{
    public class SkillModifiedValueTests
    {
        [Fact]
        public void Compute_PlayerWithSkillLevel1_ReturnsValueIndex1()
        {
            var player = new Player()
            {
                Skills = { new Skill("Smelting", 1) },
            };

            var dynamicValue = TestSkillModifiedValues.Smelting;
            var value = dynamicValue.Compute(player);

            Assert.Equal(dynamicValue.Values[1], value);
        }

        [Fact]
        public void Compute_PlayerWithoutSkill_ReturnsValueIndex0()
        {
            var player = new Player()
            {
                Skills = { new Skill("Cooking", 1) },
            };

            var dynamicValue = TestSkillModifiedValues.Smelting;
            var value = dynamicValue.Compute(player);

            Assert.Equal(dynamicValue.Values[0], value);
        }
    }
}
