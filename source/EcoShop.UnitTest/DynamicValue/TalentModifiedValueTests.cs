using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.DynamicValue
{
    public class TalentModifiedValueTests
    {
        [Fact]
        public void Compute_PlayerWithTalent_ReturnsValueIndex1()
        {
            var player = new Player()
            {
                Talents = { "SmeltingLavishResourcesTalent" },
            };

            var dynamicValue = TestTalentModifiedValues.SmeltingLavishResourcesTalent;
            var value = dynamicValue.Compute(player);

            Assert.Equal(dynamicValue.Values[1], value);
        }

        [Fact]
        public void Compute_PlayerWithoutTalent_ReturnsValueIndex0()
        {
            var player = new Player()
            {
                Talents = { "CookingLavishResourcesTalent" },
            };

            var dynamicValue = TestTalentModifiedValues.SmeltingLavishResourcesTalent;
            var value = dynamicValue.Compute(player);

            Assert.Equal(dynamicValue.Values[0], value);
        }
    }
}
