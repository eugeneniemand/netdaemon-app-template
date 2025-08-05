using System.Text.Json;
using HomeAssistantGenerated;


namespace Niemand;

[NetDaemonApp]
//[Focus]

public class Travel(IHaContext haContext, IEntities entities, IServices services, IScheduler scheduler, People people, ILogger<Travel> logger) : IAsyncInitializable
{
    private readonly Dictionary<string, DirectionOfTravel> _personDirection = [];
    private Dictionary<string, TrainDetails> _trainsHome = [];
    private Dictionary<string, TrainDetails> _trainsWork = [];
    


    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        StoreTrains(_trainsHome, entities.Sensor.TrainsToHome);
        StoreTrains(_trainsWork, entities.Sensor.TrainsToWork);

        entities.Sensor.TrainsToHome.StateChanges().Subscribe(state => StoreTrains(_trainsHome, entities.Sensor.TrainsToHome));
        entities.Sensor.TrainsToWork.StateChanges().Subscribe(state => StoreTrains(_trainsWork, entities.Sensor.TrainsToWork));

        foreach (var person in people.Persons)
        {
            person.Person.StateChanges().Subscribe(state =>
            {
                HandlePersonStateChange(person.Person.Attributes.FriendlyName, state.New?.State);
            });

            if (_personDirection.ContainsKey(person.Person.Attributes.FriendlyName))
                person.DirectionSensor.StateChanges().Subscribe(state =>
                {
                    StoreTrains(_trainsHome, entities.Sensor.TrainsToHome);
                    StoreTrains(_trainsWork, entities.Sensor.TrainsToWork);
                    
                    _personDirection[person.Person.Attributes.FriendlyName] = DirectionOfTravelMapper.Map(state.New?.State);
                });
        }

        return Task.CompletedTask;
    }

    private void HandlePersonStateChange(string personName, string? personState)
    {
        if (string.Equals(personState, "home", StringComparison.OrdinalIgnoreCase) || string.Equals(personState, "away", StringComparison.OrdinalIgnoreCase))
            return;

        logger.LogDebug($"{personName} is at {personState}");

        if (string.Equals(personState, "lst_train_line", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogInformation(_personDirection[personName] == DirectionOfTravel.Towards
                ? $"{personName} is leaving LST"
                : $"{personName} is arriving LST");
        }
        else if (string.Equals(personState, "wic_train_line", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogInformation(_personDirection[personName] == DirectionOfTravel.Towards
                ? $"{personName} is leaving WIC"
                : $"{personName} is arriving WIC");
        }
    }

    public void StoreTrains(Dictionary<string, TrainDetails> trains, NumericSensorEntity nextTrains)
    {
        var trainDetailsList = nextTrains.Attributes?.NextTrains?.Select(n => JsonSerializer.Deserialize<TrainDetails>(n.ToString())).ToList();
        if (trainDetailsList == null)
            return;
        foreach (var train in trainDetailsList)
        {
            if (!trains.ContainsKey(train.TrainUID))
                trains.Add(train.TrainUID, train);
        }

        trains = trains.Where(t => t.Value.ScheduledArrival >= DateTime.Now).ToDictionary(t => t.Key, t => t.Value);
    }
}
public enum DirectionOfTravel
{
    Arrived,
    Away,
    Stationary,
    Towards,
    Unknown
}

public static class DirectionOfTravelMapper
{
    private static readonly Dictionary<string, DirectionOfTravel> _map = new()
   {
       { "away_from", DirectionOfTravel.Away },
       { "arrived", DirectionOfTravel.Arrived },
       { "stationary", DirectionOfTravel.Stationary },
       { "towards", DirectionOfTravel.Towards },
       { "unknown", DirectionOfTravel.Unknown }
   };

    public static DirectionOfTravel Map(string? value)
    {
        return _map.TryGetValue(value, out var direction) ? direction : DirectionOfTravel.Unknown;
    }
}
