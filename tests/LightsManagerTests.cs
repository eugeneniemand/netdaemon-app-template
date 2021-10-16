using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using LightsManager;
using Moq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Daemon.Fakes;
using Xunit;

/// <summary>
///     Tests the fluent API parts of the daemon
/// </summary>
/// <remarks>
///     Mainly the tests checks if correct underlying call to "CallService"
///     has been made.
/// </remarks>
public class LightsManagerTests : RxAppMock
{
    private Manager? _app;

    public LightsManagerTests()
    {
        //    Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        //    {
        //        return Observable.Interval(span, TestScheduler)
        //                         .Subscribe(_ => action());
        //    });


        //    Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        //    {
        //        var result = new DisposableTimerResult(new CancellationToken());
        //        Observable.Timer(span, TestScheduler)
        //                  .Subscribe(_ => action(), result.Token);
        //        return result;
        //    });

        //    Setup(n => n.Entity(It.IsAny<string>())).Returns<string>(entityId =>
        //    {
        //        var m = new Mock<IRxEntityBase>();


        //        m.Setup(e => e.TurnOn(It.IsAny<object?>()))
        //         .Callback<object?>(attributes => UpdateMockState(entityId, "on", attributes));
        //        m.Setup(e => e.TurnOff(It.IsAny<object?>()))
        //         .Callback<object?>(attributes => UpdateMockState(entityId, "off", attributes));
        //        return m.Object;
        //    });
    }

    [Fact]
    public void presence_trigger_turns_on_control_entities()
    {
        // ARRANGE
        var config = new LightsManagerConfig()
        {
            Name              = "TestRoom",
            PresenceEntityIds = new List<string> { "binary_sensor.my_motion_sensor" },
            ControlEntityIds  = new List<string> { "light.my_light" },
            Timeout           = 1
        };

        MockState.Clear();
        MockState.Add(new EntityState { EntityId = config.PresenceEntityIds.First(), State = "off" });
        MockState.Add(new EntityState { EntityId = config.ControlEntityIds.First(), State  = "off" });

        var app = new Manager(Object, config);

        // ACT
        // Trigger presence to turn on control
        TriggerStateChange(config.PresenceEntityIds.First(), "off", "on");

        VerifyEntityTurnOn(config.ControlEntityIds.First(), times: Times.Once());
        VerifyEntityTurnOff(config.ControlEntityIds.First(), times: Times.Never());
    }
}