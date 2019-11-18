using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcoShop
{
    public class ValueJsonConverter : JsonConverter<IValue>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IValue).IsAssignableFrom(typeToConvert);
        }

        public override IValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Number)
                return new BasicValue { Value = reader.GetDecimal() };

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            return JsonSerializer.Deserialize<SkillModifiedValue>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IValue value, JsonSerializerOptions options)
        {
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
    }
}
