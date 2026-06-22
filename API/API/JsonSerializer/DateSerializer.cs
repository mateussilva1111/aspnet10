using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.JsonSerializer
{
    public class DateSerializer : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = "dd-MM-yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString() ?? string.Empty, _dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}