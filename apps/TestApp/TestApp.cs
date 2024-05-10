using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand.TestApp;

[NetDaemonApp]
//[Focus]
public class TestApp
{
    public TestApp(IHaContext haContext)
    {
        //var light = ((Dictionary<string, object>)haContext.Entity("light.office_skylight").Attributes)["color_mode"].ToString();
        
        // [supported_color_modes, ["brightness"]] [supported_color_modes, ["color_temp"]] [supported_color_modes, ["onoff"]]
        //alexa.Prompt("media_player.office", "Answer please?", "test_prompt");

        //alexa.PromptResponses
        //     .Where(r => r.EventId == "test_prompt")
        //     .Where(x => x.ResponseType == PromptResponseType.ResponseNumeric)
        //     .Subscribe(x =>
        //     {
        //         if (x.ResponsePersonName == "UNKNOWN")
        //             alexa.TextToSpeech("media_player.office", $"You selected the number {x.Response}");
        //         else
        //             alexa.TextToSpeech("media_player.office", $"Thank you {x.ResponsePersonName},, You selected the number {x.Response}");
        //     });

        //alexa.TextToSpeech("media_player.office", "Message 1");
        //alexa.TextToSpeech("media_player.office", "Message 2");
        //alexa.TextToSpeech("media_player.office", "3");

        //alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.office", "media_player.kitchen", "media_player.dining" }, Message = "This is a test" });
        //alexa.TextToSpeech(new Alexa.Config { Entities = new List<string> { "media_player.office", "media_player.kitchen", "media_player.dining" }, Message = "This is a longer test message to see if volume is set while message is playing" });
    }
}