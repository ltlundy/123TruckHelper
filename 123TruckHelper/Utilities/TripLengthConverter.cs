using System.Text.Json;
using System.Text.Json.Serialization;

namespace _123TruckHelper.Utilities
{
    public class TripLengthConverter : JsonConverter<TripLength>
    {
        public override TripLength Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Unexpected token type: {reader.TokenType}");
            }

            string enumValue = reader.GetString();

            if (!Enum.TryParse(enumValue, true, out TripLength result))
            {
                throw new JsonException($"Unable to parse {enumValue} to enum TripLength");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, TripLength value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
