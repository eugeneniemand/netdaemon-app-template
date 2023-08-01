using System.Collections.Generic;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks;
using NetDaemonApps.Tests.Helpers;
using Niemand.Helpers;

namespace NetDaemonApps.Tests;

public class AlexaTests
{
    [Test]
    public void DayAnnouncementTwoMessages()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "day");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig() } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.Announce("media_player.echo", "argument message");
        app.Announce(new Alexa.Config { Entity = "media_player.echo", Message = "config message" });
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<voice name='Brian'>argument message<break /><break /><break /><break />config message</voice>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "announce" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void DayTextToSpeechTwoMessages()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "day");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig() } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.TextToSpeech("media_player.echo", "argument message");
        app.TextToSpeech(new Alexa.Config { Entity = "media_player.echo", Message = "config message" });
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<voice name='Brian'>argument message<break /><break /><break /><break />config message</voice>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "tts" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void DayVolumeIsSetAndRestored()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "day");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig { DayVolume = 0.5 } } }
        };
        var oldState = new EntityState { State = "idle" }.WithAttributes(new MediaPlayerAttributes { VolumeLevel = 0.1 });
        _ctx.TriggerStateChange("media_player.echo", oldState, oldState);

        var app = _ctx.InitAlexa(config);
        // Act
        app.TextToSpeech("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert
        _ctx.VerifyCallService("media_player.volume_set", "media_player.echo", Times.Once, new MediaPlayerVolumeSetParameters { VolumeLevel = 0.5 });
        _ctx.VerifyCallService("media_player.volume_set", "media_player.echo", Times.Once, new MediaPlayerVolumeSetParameters { VolumeLevel = 0.1 });
    }

    [Test]
    public void NightAnnouncementDontWhisper()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "night");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig { NightWhisper = false } } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.Announce("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<voice name='Brian'>argument message</voice>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "announce" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void NightAnnouncementSimple()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "night");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig() } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.Announce("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<amazon:effect name='whispered'>argument message</amazon:effect>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "announce" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void NightTextToSpeechDontWhisper()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "night");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig { NightWhisper = false } } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.TextToSpeech("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<voice name='Brian'>argument message</voice>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "tts" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void NightTextToSpeechSimple()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "night");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig() } }
        };

        var app = _ctx.InitAlexa(config);
        // Act
        app.TextToSpeech("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert


        var notifyAlexaMediaParameters = new NotifyAlexaMediaParameters
        {
            Message = "<amazon:effect name='whispered'>argument message</amazon:effect>",
            Title   = null,
            Target  = "media_player.echo",
            Data    = new { type = "tts" }
        };
        _ctx.VerifyCallService("notify.alexa_media", Times.Once, notifyAlexaMediaParameters);
    }

    [Test]
    public void NightVolumeIsSetAndRestored()
    {
        var _ctx = new AppTestContext();
        _ctx.GetEntity<MediaPlayerEntity>("media_player.echo", "off");
        _ctx.GetEntity<InputSelectEntity>("input_select.house_mode", "night");

        // Arrange
        var config = new AlexaConfig
        {
            Devices = new Dictionary<string, AlexaDeviceConfig> { { "media_player.echo", new AlexaDeviceConfig { NightVolume = 0.1 } } }
        };
        var oldState = new EntityState { State = "idle" }.WithAttributes(new MediaPlayerAttributes { VolumeLevel = 0.3 });
        _ctx.TriggerStateChange("media_player.echo", oldState, oldState);

        var app = _ctx.InitAlexa(config);
        // Act
        app.TextToSpeech("media_player.echo", "argument message");
        _ctx.Scheduler.AdvanceBy(TimeSpan.FromMilliseconds(5500).Ticks);

        // Assert

        _ctx.VerifyCallService("media_player.volume_set", "media_player.echo", Times.Once, new MediaPlayerVolumeSetParameters { VolumeLevel = 0.1 });
        _ctx.VerifyCallService("media_player.volume_set", "media_player.echo", Times.Once, new MediaPlayerVolumeSetParameters { VolumeLevel = 0.3 });
    }
}

internal class TestVoiceProvider : IVoiceProvider
{
    public string GetRandomVoice() => "Brian";
}

public static class AlexaAppTestContextInstanceExtensions
{
    public static IAlexa InitAlexa(this AppTestContext ctx, AlexaConfig config) => new Alexa(ctx.HaContextMock.Object, new Entities(ctx.HaContext), new Services(ctx.HaContext), ctx.Scheduler, new TestVoiceProvider(), new FakeAppConfig<AlexaConfig>(config), new Mock<ILogger<Alexa>>().Object);
}