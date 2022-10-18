using Microsoft.Reactive.Testing;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Mocks.Moq;

namespace NetDaemonApps.Tests.Helpers;

/// <summary>
///     Helper class to handle state of the test session
/// </summary>
public class AppTestContext
{
    public GlobalConfig TestConfig { get; } = new()
    {
        DayTime           = TimeSpan.Parse("09:00:00"),
        MorningTime       = TimeSpan.Parse("09:00:00"),
        NightTimeWeekdays = TimeSpan.Parse("22:40:00"),
        NightTimeWeekends = TimeSpan.Parse("23:40:00")
    };

    public HaContextMockNSub HaContextMock { get; } = new();
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
}