namespace Niemand.TestApp;

[NetDaemonApp]
//[Focus]
public class TestApp
{
    public TestApp(IAlexa alexa)
    {
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