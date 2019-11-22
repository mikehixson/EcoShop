using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EcoShop
{
    public class Item
    {
        public string Name { get; set; }

        [JsonConverter(typeof(ValueJsonConverter2))]
        public IValue Quantity { get; set; }
    }
}
