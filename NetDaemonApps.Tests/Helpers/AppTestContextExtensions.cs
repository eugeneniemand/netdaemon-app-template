using System.Linq;
using HomeAssistantGenerated;
using Moq;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;
using Newtonsoft.Json;

namespace NetDaemonApps.Tests.Helpers;

public static class AppTestContextExtensions
{
    public static void ActivateScene(this AppTestContext ctx, string sceneName)
    {
        ctx.ChangeStateFor($"scene.{sceneName}")
           .FromState("off")
           .ToState("on");
    }

    public static IFromState ChangeStateFor(this AppTestContext ctx, string entityId) => new StateChangeContext(ctx, entityId);

    public static T? GetEntity<T>(this AppTestContext ctx, string entityId) where T : Entity => Activator.CreateInstance(typeof(T), ctx.HaContext, entityId) as T;

    public static T? GetEntity<T>(this HaContextMock ctx, string entityId) where T : Entity => Activator.CreateInstance(typeof(T), ctx.HaContext, entityId) as T;


    public static T? GetEntity<T>(this AppTestContext ctx, string entityId, string state) where T : Entity

    {
        var instance = Activator.CreateInstance(typeof(T), ctx.HaContext, entityId) as T;
        ctx.HaContextMock.TriggerStateChange(instance, state);
        return instance;
    }

    public static T? GetEntity<T>(this HaContextMock ctx, string entityId, string state) where T : Entity

    {
        var instance = Activator.CreateInstance(typeof(T), ctx.HaContext, entityId) as T;
        ctx.TriggerStateChange(instance, state);
        return instance;
    }

    public static void VerifyCallService(this AppTestContext ctx, string serviceCall, string entityId, Func<Times> times, object? data = null)
    {
        var domain = serviceCall[..serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)];
        var service = serviceCall[(serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1)..];

        ctx.HaContextMock.Verify(c => c.CallService(domain, service,
                It.Is<ServiceTarget>(s => Match(entityId, s)),
                data), times
        );
    }

    public static void VerifyCallService(this AppTestContext ctx, string serviceCall, Func<Times> times, object? data = null)
    {
        var domain = serviceCall[..serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)];
        var service = serviceCall[(serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1)..];

        ctx.HaContextMock.Verify(c => c.CallService(domain, service, null, It.Is<object>(y => VerifyHelper.AreEqualObjects(data, y))));
    }



    public static void VerifyEventRaised(this AppTestContext ctx, string eventType, Func<Times> times, object? data = null)
    {
        ctx.HaContextMock.Verify(c => c.SendEvent(eventType, data), times);
    }

    public static void VerifyInputSelect_SelectOption(this AppTestContext ctx, string entityId, string option, Times times)
    {
        ctx.HaContextMock.Verify(c => c.CallService("sensor", "select_option",
                It.Is<ServiceTarget>(x => x.EntityIds != null && x.EntityIds.First() == entityId),
                It.Is<InputSelectSelectOptionParameters>(x => x.Option == option)), times
        );
    }

    public static void VerifyLightTurnOff(this AppTestContext ctx, LightEntity entity, Func<Times> times)
    {
        ctx.VerifyCallService("light.turn_off", entity.EntityId, times, new LightTurnOffParameters());
    }

    public static void VerifyLightTurnOn(this AppTestContext ctx, LightEntity entity, Func<Times> times)
    {
        ctx.VerifyCallService("light.turn_on", entity.EntityId, times, new LightTurnOnParameters());
    }

    public static void VerifySwitchTurnOff(this AppTestContext ctx, SwitchEntity entity, Func<Times> times)
    {
        ctx.VerifyCallService("switch.turn_off", entity.EntityId, times);
    }

    public static void VerifySwitchTurnOn(this AppTestContext ctx, SwitchEntity entity, Func<Times> times)
    {
        ctx.VerifyCallService("switch.turn_on", entity.EntityId, times);
    }


    //public static void VerifyInputSelect_SelectOption(this AppTestContext ctx, string entityId, string option)
    //{
    //    ctx.HaContext.Received(1).CallService("input_select", "select_option",
    //        Arg.Is<ServiceTarget>(x
    //            => x.EntityIds != null && x.EntityIds.First() == entityId),
    //        Arg.Is<InputSelectSelectOptionParameters>(x
    //            => x.Option == option));
    //}

    //public static void VerifyInputSelect_SelectOption_NotChanged(this AppTestContext ctx, string entityId)
    //{
    //    ctx.HaContext.DidNotReceive().CallService("input_select", "select_option",
    //        Arg.Is<ServiceTarget>(x
    //            => x.EntityIds != null && x.EntityIds.First() == entityId),
    //        Arg.Any<InputSelectSelectOptionParameters>());
    //}

    public static IWithState WithEntityState<T>(this AppTestContext ctx, string entityId, T state)
    {
        var stateChangeContext = new StateChangeContext(ctx, entityId);
        stateChangeContext.WithEntityState(entityId, state);
        return stateChangeContext;
    }

    private static bool Match(string s, ServiceTarget x)
    {
        return x == null || (x.EntityIds != null && x.EntityIds.Any(id => id == s));
    }
}

public static class VerifyHelper
{
    public static bool AreEqualObjects(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson = JsonConvert.SerializeObject(actual);

        return expectedJson == actualJson;
    }
}