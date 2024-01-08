using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Reactive.Testing;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;

namespace AwesomeNetdaemon.Test.TestUtils;

public class StateChangeManager(IHaContext haContextMock, TestScheduler testScheduler)
{
    public ReadOnlyCollection<TestServiceCall> ServiceCalls => ((HaContextMockImpl)haContextMock).ServiceCalls;

public StateChangeManager Change(Entity entity, string newStatevalue, object? attributes = null)
{
    ((HaContextMockImpl)haContextMock).TriggerStateChange(entity, newStatevalue, attributes);
    return this;
}

public StateChangeManager Change(NumericSensorEntity entity, double newStatevalue, object? attributes = null) =>
    Change(entity, newStatevalue.ToString(CultureInfo.InvariantCulture), attributes);

public StateChangeManager AdvanceTo(DateTime dateTime)
{
    testScheduler.AdvanceTo(dateTime.ToUniversalTime().Ticks);
    return this;
}

public StateChangeManager AdvanceTo(TimeOnly timeOnly)
{
    testScheduler.AdvanceTo(new DateTime(DateOnly.FromDateTime(testScheduler.Now.Date), timeOnly).ToUniversalTime().Ticks);
    return this;
}

public StateChangeManager AdvanceTo(DateOnly dateOnly)
{
    testScheduler.AdvanceTo(new DateTime(dateOnly, TimeOnly.FromDateTime(testScheduler.Now.DateTime)).ToUniversalTime().Ticks);
    return this;
}

public StateChangeManager AdvanceDays(int days)
{
    testScheduler.AdvanceBy(TimeSpan.FromDays(days).Ticks);
    return this;
}
}