using System.Text.Json.Serialization;


namespace Niemand;

public class TrainDetails
{
    [JsonPropertyName("origin_name")]
    public string OriginName { get; init; }

    [JsonPropertyName("destination_name")]
    public string DestinationName { get; init; }

    [JsonPropertyName("service_uid")]
    public string ServiceUid { get; init; }

    [JsonPropertyName("scheduled")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? Scheduled { get; init; }

    [JsonPropertyName("estimated")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? Estimated { get; init; }

    /// <summary>
    /// How many minutes early/late will the train deptart 
    /// </summary>
    [JsonPropertyName("delay")]
    public double Delay { get; init; }
    
    /// <summary>
    /// How many minutes from now is the Estimated time
    /// </summary>
    [JsonPropertyName("minutes")]    
    public double Minutes { get; init; }

    [JsonPropertyName("platform")]
    public string Platform { get; init; }

    [JsonPropertyName("operator_name")]
    public string OperatorName { get; init; }

    [JsonPropertyName("stops_of_interest")]
    public List<string> StopsOfInterest { get; init; }

    [JsonPropertyName("scheduled_arrival")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? ScheduledArrival { get; init; }

    [JsonPropertyName("estimate_arrival")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? EstimateArrival { get; init; }

    /// <summary>
    /// How many minutes early/late will the train arrive 
    /// </summary>
    [JsonPropertyName("arrival_delay")]
    public double ArrivalDelay { get; init; }

    [JsonPropertyName("journey_time_mins")]
    public double JourneyTimeMins { get; init; }

    [JsonPropertyName("stops")]
    public int Stops { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    public string TrainUID => $"{OriginName}-{DestinationName}-{Platform}-{Scheduled:HH:mm}-{ScheduledArrival:HH:mm}";
}
