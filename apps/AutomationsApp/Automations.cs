using Microsoft.Extensions.Hosting;

namespace Niemand.AutomationsApp;

[NetDaemonApp]
public class Automations
{
    private readonly IEntities _entities;
    private          DateTime  _aaronLastMotion;

    public Automations(IHaContext haContext, IEntities entities, Alexa alexa, IHostEnvironment hostEnvironment)
    {
        _entities = entities;

        //KidsOutOfBed(haContext, entities);
    }

    private void Dishwasher(IHaContext haContext, IEntities entities)
    {
        var dishwasherPower = (SensorEntity)haContext.Entity("sensor.dishwasher_power");
    }

    private void KidsOutOfBed(IHaContext haContext, IEntities entities)
    {
        BinarySensorEntity firstSensor;
        BinarySensorEntity secondSensor;
        MediaPlayerEntity? target;

        //if (!hostEnvironment.IsDevelopment())
        //{
        firstSensor  = entities.BinarySensor.AaronMotion;
        secondSensor = entities.BinarySensor.MasterMotion;
        target       = entities.MediaPlayer.Master;
        //}
        //else
        //{
        //    firstSensor  = entities.BinarySensor.Study;
        //    secondSensor = entities.BinarySensor.Hallway;
        //    target       = entities.MediaPlayer.Office;
        //}

        firstSensor.StateChanges().Where(change => change.New.IsOn()).Subscribe(_ => _aaronLastMotion = DateTime.Now);
        secondSensor.StateChanges().Where(change => change.Old.IsOff() && change.New.IsOn()).Subscribe(change =>
        {
            if (DateTime.Now - _aaronLastMotion <= TimeSpan.FromMinutes(2) && _entities.AlarmControlPanel.Alarmo.State == "armed_nightr") haContext.SendEvent("kids_out_of_bed_event");
            ;
        });
    }
}