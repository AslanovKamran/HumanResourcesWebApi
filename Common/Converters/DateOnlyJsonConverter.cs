using System.Text.Json.Serialization;
using System.Text.Json;

namespace HumanResourcesWebApi.Common.Converters
{

    public class DateOnlyJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Handle the case when the reader returns null
            var dateString = reader.GetString();
            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new JsonException("Invalid or missing date.");
            }

            return DateTime.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write the date part only in 'yyyy-MM-dd' format
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
