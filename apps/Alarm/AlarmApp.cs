namespace Niemand.TestApp;

[NetDaemonApp]
//[Focus]
public class Alarm
{
    public Alarm(IEntities entities, IAlexa alexa)
    {
        entities.AlarmControlPanel.Alarmo.StateChanges().Subscribe(s =>
        {
            switch (s.New.State)
            {
                case "armed_night":
                    var reminderMessages = new List<string>();
                    if (entities.InputBoolean.DryerReminder.IsOn())
                        reminderMessages.Add("Dryer");
                    if (entities.InputBoolean.WashingReminder.IsOn())
                        reminderMessages.Add("Washer");
                    if (entities.InputBoolean.DryerReminder.IsOn())
                        reminderMessages.Add("Dishwasher");

                    var reminderMessage = reminderMessages.Any() ? string.Join(",", reminderMessages) + " is ready but not turned on." : "";
                    alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = $"{reminderMessage} Alarm armed" });
                    break;
                case "disarmed":
                    alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = "Alarm disarmed" });
                    break;
            }
        });
    }
}