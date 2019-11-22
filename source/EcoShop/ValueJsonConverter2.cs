using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace EcoShop
{
    public class ValueJsonConverter2 : JsonConverter<IValue>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IValue).IsAssignableFrom(typeToConvert);
        }

        public override IValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();
            
            var dynamicValue = JsonSerializer.Deserialize<DynamicValue>(ref reader, options);

            return Convert(dynamicValue);
        }

        private IValue Convert(DynamicValue dynamicValue)
        {
            switch (dynamicValue.Type)
            {
                case "ConstantValue":
                    return new BasicValue { Value = dynamicValue.Values[0] };

                case "SkillModifiedValue":
                    return new SkillModifiedValue { Skill = dynamicValue.ModifierName, Values = dynamicValue.Values };

                case "TalentModifiedValue":
                    return new TalentModifiedValue { Talent = dynamicValue.ModifierName, Values = dynamicValue.Values };

                case "MultiDynamicValue":
                    return new MultiDynamicValue { Operation = dynamicValue.Operation, Children = dynamicValue.Children.Select(c => Convert(c)).ToArray() };
            }

            throw new Exception($"Unexpected DynamicValue Type: {dynamicValue.Type}.");
        }

        public override void Write(Utf8JsonWriter writer, IValue value, JsonSerializerOptions options)
        {
            //todo:

            switch (value)
            {
                case BasicValue v:
                    writer.WriteNumberValue(v.Value);
                    break;

                case SkillModifiedValue v:
                    JsonSerializer.Serialize(writer, v, options);
                    break;
            }
        }

        private class DynamicValue
        {
            public string Type { get; set; }
            public string ModifierName { get; set; }
            public string Operation { get; set; }
            public decimal[] Values { get; set; }
            public DynamicValue[] Children { get; set; }
        }
    }
}
