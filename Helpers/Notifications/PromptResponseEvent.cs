using System.Text.Json.Serialization;

namespace NetDaemon.Helpers;

public record PromptResponseEvent
{
    [JsonPropertyName("event_response")] public object? Response { get; init; }

    [JsonPropertyName("event_response_type")]
    public PromptResponseType? ResponseType { get; init; }

    [JsonPropertyName("event_id")] public string? EventId { get; init; }
    [JsonPropertyName("event_person_id")] public string? ResponsePersonId { get; init; }
}

public record PromptResponse
{
    public object Response { get; init; }
    public PromptResponseType ResponseType { get; init; }
    public string EventId { get; init; }
    public string ResponsePersonId { get; init; }
    public string ResponsePersonName { get; init; } 
}