using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EcoShop.UnitTest.DynamicValue
{
    public class ConstantValueTests
    {
        [Fact]
        public void Compute_AnyPlayer_IsValid()
        {
            var player = new Player
            {

            };

            var dynamicValue = new ConstantValue() { Value = 1 };
            var value = dynamicValue.Compute(player);

            Assert.Equal(1, value);
        }
    }
}
