using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemonApps.Tests.Helpers;
using Niemand.Energy;
using Niemand.Helpers;

namespace NetDaemonApps.Tests;

public class BatteriesTests
{
    [Test]
    public void NotifiesWhenBatteriesAreFlat()
    {
        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(new DateTime(2022, 11, 07, 6, 59, 59).Ticks);
        var entrance = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_entrance_battery", "100");
        var office   = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_office_battery", "0");
        var landing  = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_landing_battery", "0");
        var app      = ctx.InitBatteries();
        ctx.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        ctx.VerifyCallService("notify.twinstead", Times.Once, new NotifyTwinsteadParameters { Message = "The following Radiator batteries are flat, landing, office" });
    }

    [Test]
    public void NotifiesWhenBatteriesAreLow()
    {
        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(new DateTime(2022, 11, 07, 6, 59, 59).Ticks);
        var entrance = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_entrance_battery", "100");
        var office   = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_office_battery", "10");
        var landing  = ctx.GetEntity<NumericSensorEntity>("sensor.wiser_itrv_landing_battery", "10");
        var app      = ctx.InitBatteries();
        ctx.Scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        ctx.VerifyCallService("notify.twinstead", Times.Once, new NotifyTwinsteadParameters { Message = "The following Radiator batteries are low, landing, office" });
    }
}

public static class BatteriesAppTestContextInstanceExtensions
{
    public static BatteriesApp InitBatteries(this AppTestContext ctx) => new(ctx.HaContext, ctx.Scheduler, new Mock<ILogger<BatteriesApp>>().Object, new Services(ctx.HaContext), new Mock<IAlexa>().Object);
}