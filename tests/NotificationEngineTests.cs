using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NetDaemon.Common;
using NetDaemon.Daemon.Fakes;
using Niemand;
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public class NotificationEngineTests : RxAppMock
{
    public NotificationEngineTests()
    {
        Setup(n => n.States).Returns(MockState);
        Setup(e => e.SetState(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Callback<string, object, object, bool>((entityId, state, attributes, waitForResponse) =>
                UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
    }

    private void UpdateMockState(string entityId, string newState, object? attributes)
    {
        var state = MockState.FirstOrDefault(e => e.EntityId == entityId);
        if (state == null) return;
        MockState.Remove(state);
        MockState.Add(new EntityState { EntityId = entityId, State = newState, Attribute = attributes });
    }

    private void AssertMessages(NotificationEngineImpl app, string voiceMessage, string instantMessage)
    {
        Assert.Equal($"<s>{voiceMessage}</s>", app.VoiceMessage);
        Assert.Equal(instantMessage + Environment.NewLine, app.InstantMessage);
    }

    private void AssertMessages(NotificationEngineImpl app, List<string> voiceMessages, List<string> instantMessages)
    {
        Assert.Equal(string.Join("", voiceMessages.Select(voiceMessage => $"<s>{voiceMessage}</s>")), app.VoiceMessage);
        Assert.Equal(string.Join("", instantMessages.Select(instantMessage => instantMessage + Environment.NewLine)),
            app.InstantMessage);
    }

    [Fact]
    public void VoiceMessageIsSentWhenStateIsMatched()
    {
        var config = new NotificationEngineConfig()
        {
            EntityId = "sensor.some_sensor",
            VoiceMsg = "Your car's doors are open"
        };

        MockState.Add(new EntityState() { EntityId = config.EntityId, State = "off" });

        var app = new NotificationEngineImpl(Object,
            new Dictionary<string, NotificationEngineConfig>() { { "test_message", config } });
        app.Initialize();
        TriggerStateChange(config.EntityId, "off", "on");
        AssertMessages(app, "Your car's doors are open", "Car doors are open");
        VerifyCallService("notify", "alexa_media");
    }

    //[Fact]
    //public void GenerateMessagesConcatenatesAllMessages()
    //{
    //    var config = new NotificationEngineConfig(carDoorsEntityId: "sensor.doors");

    //    MockState.Add(new EntityState() { EntityId = config.CarDoorsEntityId, State = "on" });
    //    MockState.Add(new EntityState() { EntityId = config.CarLockedEntityId, State = "on" });

    //    var app = new NotificationEngineImpl(Object, config);
    //    app.Initialize();
    //    app.Notify(new List<string>() { "CarLocked", "CarOpen" });

    //    AssertMessages(app, new List<string> { "Your car is not locked", "Your car's doors are open" },
    //        new List<string> { "Car is not locked", "Car doors are open" });
    //    VerifyCallService("notify", "alexa_media");
    //}
}