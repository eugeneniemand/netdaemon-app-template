using System.Text.Json.Serialization;

namespace NetDaemon.Helpers;

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