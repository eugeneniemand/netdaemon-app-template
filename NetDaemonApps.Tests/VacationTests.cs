using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using HomeAssistantGenerated;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.Client;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemonApps.Tests.Helpers;
using Niemand;

namespace NetDaemonApps.Tests;

public class VacationTests
{
    [Test]
    public async Task LightsTurnOnAndOffForASingleDayAndEntity()
    {
        var nowDate = "2023-05-28 22:30:10".ToDate();

        const string lightEntityId = "light.test_light";

        var response7days = new LampEntityCollection
        {
            new()
            {
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-20 22:30:10".ToDate(), state = "on" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-21 22:32:11".ToDate(), state = "on" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-21 22:34:12".ToDate(), state = "off" }
            }
        };


        var apiMock = new Mock<IHomeAssistantApiManager>();
        var day7Api = $"history/period/{nowDate.AddDays(-7).Date:s}?filter_entity_id={HttpUtility.UrlEncode(lightEntityId)}&no_attributes";
        apiMock.Setup(m => m.GetApiCallAsync<LampEntityCollection>(day7Api, default)).ReturnsAsync(response7days);

        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(nowDate.Ticks);
        var app = ctx.InitVacation(new VacationAppConfiguration
        {
            Lights = new List<string> { lightEntityId }
        }, apiMock.Object);
        await app.EnableVacationMode();
        ctx.Scheduler.AdvanceTo("2023-05-28 22:35:00".ToDate().Ticks);

        ctx.VerifyCallService("light.turn_on", lightEntityId, Times.Once, new LightTurnOnParameters { BrightnessPct = 100 });
        ctx.VerifyCallService("light.turn_off", lightEntityId, Times.Once, new LightTurnOffParameters());
    }

    [Test]
    public async Task LightsTurnOnAndOffForASingleDayAndMultiEntities()
    {
        var nowDate = "2023-06-04 10:20:10".ToDate();


        var responseKitchen = new LampEntityCollection
        {
            new()
            {
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 10:21:10".ToDate(), state = "on" },
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 10:29:10".ToDate(), state = "off" },
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 10:30:10".ToDate(), state = "on" },
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 10:40:10".ToDate(), state = "off" },
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 10:53:10".ToDate(), state = "on" },
                new LampEntity { entity_id = "light.kitchen", last_changed = "2023-05-28 11:06:10".ToDate(), state = "off" }
            }
        };

        var responseUtility = new LampEntityCollection
        {
            new()
            {
                new LampEntity { entity_id = "light.utility", last_changed = "2023-05-28 10:23:10".ToDate(), state = "on" }
            }
        };


        var apiMock    = new Mock<IHomeAssistantApiManager>();
        var kitchenApi = $"history/period/{nowDate.AddDays(-7).Date:s}?filter_entity_id={HttpUtility.UrlEncode("light.kitchen")}&no_attributes";
        var utilityApi = $"history/period/{nowDate.AddDays(-7).Date:s}?filter_entity_id={HttpUtility.UrlEncode("light.utility")}&no_attributes";
        apiMock.Setup(m => m.GetApiCallAsync<LampEntityCollection>(kitchenApi, default)).ReturnsAsync(responseKitchen);
        apiMock.Setup(m => m.GetApiCallAsync<LampEntityCollection>(utilityApi, default)).ReturnsAsync(responseUtility);

        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(nowDate.Ticks);
        var app = ctx.InitVacation(new VacationAppConfiguration
        {
            Lights = new List<string> { "light.kitchen", "light.utility" }
        }, apiMock.Object);
        await app.EnableVacationMode();
        ctx.Scheduler.AdvanceTo("2023-06-04 11:07:00".ToDate().Ticks);

        ctx.VerifyCallService("light.turn_on", "light.kitchen", Times.Exactly(3), new LightTurnOnParameters { BrightnessPct = 100 });
        ctx.VerifyCallService("light.turn_on", "light.utility", Times.Once, new LightTurnOnParameters { BrightnessPct       = 100 });
        ctx.VerifyCallService("light.turn_off", "light.kitchen", Times.Exactly(3), new LightTurnOffParameters());
    }

    [Test]
    public async Task LightsTurnOnAndOffForMultiDay()
    {
        var nowDate = "2023-05-28 22:30:10".ToDate();

        const string lightEntityId = "light.test_light";

        var response7days = new LampEntityCollection
        {
            new()
            {
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-20 22:30:10".ToDate(), state = "on" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-21 22:32:11".ToDate(), state = "on" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-21 22:34:12".ToDate(), state = "off" }
            }
        };

        var response6days = new LampEntityCollection
        {
            new()
            {
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-21 22:30:10".ToDate(), state = "off" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-22 22:35:14".ToDate(), state = "on" },
                new LampEntity { entity_id = lightEntityId, last_changed = "2023-05-22 22:36:13".ToDate(), state = "off" }
            }
        };

        var apiMock = new Mock<IHomeAssistantApiManager>();
        var day7Api = $"history/period/{nowDate.AddDays(-7).Date:s}?filter_entity_id={HttpUtility.UrlEncode(lightEntityId)}&no_attributes";
        var day6Api = $"history/period/{nowDate.AddDays(-6).Date:s}?filter_entity_id={HttpUtility.UrlEncode("light.test_light")}&no_attributes";
        apiMock.Setup(m => m.GetApiCallAsync<LampEntityCollection>(day7Api, default)).ReturnsAsync(response7days);
        apiMock.Setup(m => m.GetApiCallAsync<LampEntityCollection>(day6Api, default)).ReturnsAsync(response6days);

        var ctx = new AppTestContext();
        ctx.Scheduler.AdvanceTo(nowDate.Ticks);
        var app = ctx.InitVacation(new VacationAppConfiguration
        {
            Lights = new List<string> { lightEntityId }
        }, apiMock.Object);
        await app.EnableVacationMode();
        ctx.Scheduler.AdvanceTo("2023-05-28 22:36:00".ToDate().Ticks);

        ctx.VerifyCallService("light.turn_on", lightEntityId, Times.Once, new LightTurnOnParameters { BrightnessPct = 100 });
        ctx.VerifyCallService("light.turn_off", lightEntityId, Times.Once, new LightTurnOffParameters());
    }
}

public static class VacationAppTestContextInstanceExtensions
{
    public static VacationApp InitVacation(this AppTestContext ctx, VacationAppConfiguration config, IHomeAssistantApiManager apiMock) => new(ctx.HaContext, new Mock<ILogger<VacationApp>>().Object, new FakeAppConfig<VacationAppConfiguration>(config), new Entities(ctx.HaContext), new Services(ctx.HaContext), apiMock, new Mock<IMqttEntityManager>().Object, ctx.Scheduler);

    public static DateTime ToDate(this string date) => DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
}

//    [10:05:00 INF] Next          state change is on at 10:21 for light.kitchen<Niemand.VacationApp>
//    [10:22:00 INF] Next          state change is on at 10:23 for light.utility<Niemand.VacationApp>
//    [10:24:00 INF] Next          state change is off at 10:29 for light.kitchen<Niemand.VacationApp>
//    [10:30:00 INF] Next          state change is on at 10:30 for light.kitchen<Niemand.VacationApp>
//    [10:30:00 INF] light.kitchen turned on <Niemand.VacationApp>
//    [10:31:00 INF] Next          state change is off at 10:40 for light.kitchen<Niemand.VacationApp>
//    [10:41:00 INF] Next          state change is on at 10:53 for light.kitchen<Niemand.VacationApp>
//    [10:54:00 INF] Next          state change is off at 11:06 for light.kitchen<Niemand.VacationApp>