using Niemand.Helpers;
using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class JaydenLight
{
    public JaydenLight(Entities entities, ILogger<JaydenLight> logger)
    {
        entities.BinarySensor.JaydenMotion.StateChanges()
            .Where(e => e.New.IsOn())
            .Subscribe(state => { 
                entities.Light.JaydenFloor.TurnOn(new LightTurnOnParameters { BrightnessPct = 1, ColorTempKelvin = 2700 });
                logger.LogInformation("Jayden floor light turned on due to motion detected.");
        });

        // Turn the light off 30 minutes after no motion; cancel if motion resumes
        var motionChanges = entities.BinarySensor.JaydenMotion.StateChanges();

        motionChanges
            .Select(e => e.New.IsOn())
            .DistinctUntilChanged()
            .Select(isOn => isOn ? Observable.Never<long>() : Observable.Timer(TimeSpan.FromMinutes(30)))
            .Switch()
            .Subscribe(_ => {
                entities.Light.JaydenFloor.TurnOff();
                logger.LogInformation("Jayden floor light turned off after 30 minutes without motion.");
        });
    }
}
