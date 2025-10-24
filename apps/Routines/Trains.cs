namespace Niemand;

public static class Trains
{
    public static void SetInputTextForTrain(InputTextEntity inputText, NumericSensorEntity trainSensor)
    {
        var start = trainSensor.Attributes?.JourneyStart;
        var end = trainSensor.Attributes?.JourneyEnd;
        var nextTrains = trainSensor.Attributes?.NextTrains?
            .Select(n => JsonSerializer.Deserialize<TrainDetails>(n.ToString()))
            .Where(t => string.Equals(t.OperatorName, "Elizabeth line", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (nextTrains?.Count != 0 && trainSensor.EntityState != null)
        {
            var tableRows = string.Join("\n", nextTrains?.Select(nt => $"|{nt.Platform}|{nt.Estimated:HH:mm}|{nt.EstimateArrival:HH:mm}|{nt.Status}|") ?? []);
            var lastUpdated = trainSensor.EntityState.LastChanged.Value;
            var markdownTemplate = $"### {start}->{end} in {nextTrains[0].Minutes} min\n|**Plat**|**Depart**|**Arrival**|**Status**|\n|---|---|---|---|\n{tableRows}\n\nUpdated: {lastUpdated:HH:mm}";
            inputText.SetValue(new InputTextSetValueParameters() { Value = markdownTemplate });
        }
        else
            inputText.SetValue(new InputTextSetValueParameters() { Value = $"Unknown Train Data" });
    }
}
