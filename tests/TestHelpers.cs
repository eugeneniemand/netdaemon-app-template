using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.Reactive.Testing;
using Moq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Daemon.Fakes;
using Newtonsoft.Json;

public static class TestHelpers
{
    /// <summary>
    /// Time in "HH:mm:ss" format
    /// </summary>
    /// <param name="testTime"></param>
    /// <returns></returns>
    public static DateTime DateTimeFromString(string testTime)
    {
        return DateTime.ParseExact(testTime, "HH:mm:ss", new DateTimeFormatInfo());
    }

    public static void AdvanceTo(this TestScheduler scheduler, string time)
    {
        scheduler.AdvanceTo(DateTimeFromString(time).Ticks);
    }

    public static bool AreEqualObjects(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson   = JsonConvert.SerializeObject(actual);

        return expectedJson == actualJson;
    }


    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            expando.Add(property.Name, property.GetValue(value));

        return expando as ExpandoObject;
    }

    public static void ShimSetup(this RxAppMock mock)
    {
        mock.Setup(s => s.RunEvery(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            return Observable.Interval(span, mock.TestScheduler)
                             .Subscribe(_ => action());
        });

        mock.Setup(n => n.States).Returns(mock.MockState);
        mock.Setup(e => e.SetState(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Returns<string, object, object, bool>((entityId, state, attributes, waitForResponse) =>
                UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
        mock.Setup(s => s.RunIn(It.IsAny<TimeSpan>(), It.IsAny<Action>())).Returns<TimeSpan, Action>((span, action) =>
        {
            var result = new DisposableTimerResult(new CancellationToken());
            Observable.Timer(span, mock.TestScheduler)
                      .Subscribe(_ => action(), result.Token);
            return result;
        });

        mock.Setup(n => n.Entity(It.IsAny<string>())).Returns<string>(entityId =>
        {
            var m = new Mock<IRxEntityBase>();
            m.Setup(n => n.StateChanges)
             .Returns(mock.StateChangesObservable.Where(f =>
                 f.New?.EntityId == entityId && f.New?.State != f.Old?.State));
            m.Setup(n => n.StateAllChanges).Returns(mock.StateChangesObservable.Where(f => f.New?.EntityId == entityId));
            m.Setup(e => e.TurnOn(It.IsAny<object?>()))
             .Callback<object?>(attributes => UpdateMockState(entityId, "on", attributes));
            m.Setup(e => e.TurnOff(It.IsAny<object?>()))
             .Callback<object?>(attributes => UpdateMockState(entityId, "off", attributes));
            m.Setup(e => e.SetState(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<bool>()))
             .Callback<object, object, bool>((state, attributes, waitForResponse) =>
                 UpdateMockState(entityId, state.ToString() ?? string.Empty, attributes));
            return m.Object;
        });

        EntityState UpdateMockState(string entityId, string newState, object? attributes)
        {
            var state = mock.MockState.FirstOrDefault(e => e.EntityId == entityId);
            if (state != null)
                mock.MockState.Remove(state);

            var entityState = new EntityState { EntityId = entityId, State = newState, Attribute = attributes };
            mock.MockState.Add(entityState);
            return entityState;
        }
    }
}