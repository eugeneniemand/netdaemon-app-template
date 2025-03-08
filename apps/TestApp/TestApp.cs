using Microsoft.AspNetCore.Mvc.Formatters;
using NetDaemon.Client;
using NetDaemon.Helpers;
using Niemand.Helpers;

namespace Niemand.TestApp;

[NetDaemonApp]
[Focus]
public class TestApp
{
    public TestApp(IHaContext haContext, IEntities entities, IHomeAssistantApiManager api, ILogger<TestApp> logger, IAlexa alexa)
    {
        // var resp = api.GetApiCallAsync<object>("history/period?filter_entity_id=light.kitchen", CancellationToken.None).GetAwaiter().GetResult();
        // var x    = 1;
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

        //alexa.PlaySound( entities.MediaPlayer.Office, "amzn_sfx_scifi_alarm_04");
        
        // var vars = System.Environment.GetEnvironmentVariables();
        // logger.LogInformation("Environment Vars:{vars}", vars);
        //
        // var secret = Environment.GetEnvironmentVariable("some_password");
        // logger.LogInformation("Secret:{secret}", secret);
    }
}