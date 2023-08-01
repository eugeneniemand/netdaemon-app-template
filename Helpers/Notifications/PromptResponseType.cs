using System.Text.Json.Serialization;

namespace Niemand.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PromptResponseType
{
    ResponseYes,
    ResponseNo,
    ResponseNone,
    ResponseSelect,
    ResponseNumeric,
    ResponseDuration
}