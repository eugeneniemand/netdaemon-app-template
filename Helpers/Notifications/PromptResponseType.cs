using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetDaemon.Helpers;

public class PromptResponseTypeConverter : JsonConverter<PromptResponseType>
{
    public override PromptResponseType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Unexpected token {reader.TokenType} when parsing enum");

        var stringValue = reader.GetString();
        if (string.IsNullOrEmpty(stringValue))
            throw new JsonException("Empty string cannot be parsed as PromptResponseType");

        // Normalize the input by removing underscores for matching
        var normalizedValue = stringValue.Replace("_", string.Empty);

        // Try to match enum member names case-insensitively
        foreach (var field in typeof(PromptResponseType).GetFields())
        {
            if (field.Name.Equals(normalizedValue, StringComparison.OrdinalIgnoreCase))
                return (PromptResponseType)field.GetValue(null)!;
        }

        // If no match found, return Unknown as fallback
        return PromptResponseType.ResponseUnknown;
    }

    public override void Write(Utf8JsonWriter writer, PromptResponseType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

[JsonConverter(typeof(PromptResponseTypeConverter))]
public enum PromptResponseType
{
    ResponseYes,
    ResponseNo,
    ResponseNone,
    ResponseSelect,
    ResponseNumeric,
    ResponseDuration,
    ResponseUnknown
}