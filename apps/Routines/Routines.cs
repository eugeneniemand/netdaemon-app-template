namespace Niemand;

[NetDaemonApp]
[Focus]
public class Routines
{
    public Routines(IEntities entities, IServices services, IScheduler scheduler, People people, ILogger<Routines> logger)
    {
        // Arriving Home
        foreach (var person in people.Persons)
            person.Person.StateChanges()
                  .Where(change => string.Equals(change.Old.State, "not_home", StringComparison.InvariantCultureIgnoreCase))
                  .Where(change => string.Equals(change.New.State, "home", StringComparison.InvariantCultureIgnoreCase))
                  .Subscribe(change =>
                  {
                      var person = change.Entity.EntityId.Replace("person.", "");
                      person = char.ToUpper(person[0]) + person[1..];

                      logger.LogInformation("{person} is home", person);

                      if (!string.Equals(entities.AlarmControlPanel.Alarmo.State, "disarmed"))
                      {
                          logger.LogInformation("Disarming Alarm - {person} is home", person);
                          entities.AlarmControlPanel.Alarmo.AlarmDisarm();
                      }

                      if (string.Equals(entities.Sun.Sun.State, "below_horizon", StringComparison.InvariantCultureIgnoreCase))
                      {
                          logger.LogInformation("Turning on Outside Front Lights");
                          entities.Light.OutsideFront.TurnOn();
                          entities.Light.Porch.TurnOn();
                          entities.Light.Entrance.TurnOn();
                          entities.Light.Hallway.TurnOn();
                      }

                      services.Notify.Twinstead($"{person} is home");
                  });

        // Leaving Home
        people.Persons.Select(p => p.Person.StateChanges()
                                    .StartWith(new StateChange(p.Person, p.Person.EntityState, p.Person.EntityState)))
              .CombineLatest()
              .Do(list => logger.LogDebug("Leaving Home: {states}", list.Select( p => new {p.Entity.EntityId , p.Entity.State} )))
              .Where(change => change.All(c => !string.Equals(c.New?.State, "home", StringComparison.InvariantCultureIgnoreCase)))
              .Subscribe(change =>
              {
                  logger.LogInformation("Everyone has left home");

                  if (string.Equals(entities.AlarmControlPanel.Alarmo.State, "disarmed"))
                  {
                      logger.LogInformation("Arming Alarm");
                      entities.AlarmControlPanel.Alarmo.AlarmArmAway();
                  }

                  services.Notify.Twinstead("Good bye, alarm armed");
              });

        // Coming Down Stairs When Alarm is Armed Night
        var motionSensors = new[]
        {
            entities.BinarySensor.Hallway.StateChanges(),
            entities.BinarySensor.LandingMotion.StateChanges()
        };

        Observable.Merge(motionSensors)
                  .Select(e => e.New.State)
                  .Throttle(TimeSpan.FromSeconds(1), scheduler)
                  .Where(s => string.Equals(s, "on", StringComparison.InvariantCultureIgnoreCase))
                  .Subscribe(_ =>
                  {
                      if (!string.Equals(entities.AlarmControlPanel.Alarmo.State, "armed_night")) return;

                      logger.LogInformation("Disarming Alarm - Motion on stairs");
                      entities.AlarmControlPanel.Alarmo.AlarmDisarm();
                  });
    }
}

public record PersonDetails(PersonEntity Person, string Home);

public class People(IEntities entities)
{
    public IEnumerable<PersonDetails> Persons { get; } = new[]
    {
        new PersonDetails(entities.Person.Eugene, "home"),
        new PersonDetails(entities.Person.Hailey, "home"),
        new PersonDetails(entities.Person.Aubrecia, "mum_home")
    };
}