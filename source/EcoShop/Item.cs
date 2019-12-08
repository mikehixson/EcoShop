using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EcoShop
{
    public class Item
    {
        public string Name { get; set; }

        [JsonConverter(typeof(DynamicValueJsonConverter))]
        public IDynamicValue Quantity { get; set; }
    }
}
