using System.Text.Json.Serialization;


namespace Niemand;

public class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, "dd-MM-yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
        }
        return null; // Return null for invalid or missing values  
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("o")); // Use ISO 8601 format
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
