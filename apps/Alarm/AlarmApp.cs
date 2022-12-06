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
                    alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = "Alarm armed" });
                    break;
                case "disarmed":
                    alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.master", "media_player.office" }, Message = "Alarm disarmed" });
                    break;
            }
        });
    }
}