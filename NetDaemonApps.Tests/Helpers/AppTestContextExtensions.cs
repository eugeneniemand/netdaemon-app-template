using System.Linq;
using Moq;
using NetDaemon.HassModel.Entities;
using NetDaemon.HassModel.Mocks.Moq;

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
        var domain  = serviceCall[..serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)];
        var service = serviceCall[( serviceCall.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1 )..];

        ctx.HaContextMock.Verify(c => c.CallService(domain, service,
                It.Is<ServiceTarget>(x => x.EntityIds != null && x.EntityIds.First() == entityId),
                data), times
        );
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
}