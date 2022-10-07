using System.Text.Json.Serialization;

namespace Niemand;

public record NotificationResponseEvent
{
    [JsonPropertyName("event_id")] public string? EventId { get; init; }

    [JsonPropertyName("event_response")] public string? Response { get; init; }

    [JsonPropertyName("event_response_type")]
    public string? ResponseType { get; init; }
}