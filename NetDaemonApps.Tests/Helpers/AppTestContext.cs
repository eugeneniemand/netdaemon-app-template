using Microsoft.Reactive.Testing;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;

namespace NetDaemonApps.Tests.Helpers;

/// <summary>
///     Helper class to handle state of the test session
/// </summary>
public class AppTestContext
{
    public HaContextMock HaContextMock { get; } = new();
    public IHaContext HaContext => HaContextMock.HaContext;
    public TestScheduler Scheduler { get; } = new();

    public void AdvanceTimeBy(long absoluteTime)
    {
        Scheduler.AdvanceBy(absoluteTime);
    }

    public void AdvanceTimeTo(long absoluteTime)
    {
        Scheduler.AdvanceTo(absoluteTime);
    }

    public static AppTestContext New() => new();

    public static AppTestContext New(DateTime startTime)
    {
        var ctx = new AppTestContext();
        ctx.SetCurrentTime(startTime);
        return ctx;
    }

    public void SetCurrentTime(DateTime time)
    {
        AdvanceTimeTo(time.Ticks);
    }

    public void TriggerEvent(Event @event)
    {
        HaContextMock.TriggerEvent(@event);
    }

    public void TriggerStateChange(Entity entity, string newStatevalue, object? attributes = null)
    {
        HaContextMock.TriggerStateChange(entity, newStatevalue, attributes);
    }

    public void TriggerStateChange(Entity entity, EntityState newState)
    {
        HaContextMock.TriggerStateChange(entity.EntityId, newState);
    }

    public void TriggerStateChange(string entityId, EntityState oldState, EntityState newState)
    {
        HaContextMock.TriggerStateChange(entityId, oldState, newState);
    }
}