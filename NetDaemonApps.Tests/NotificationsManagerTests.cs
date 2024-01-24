using System.Threading.Tasks;
using HomeAssistantGenerated;
using Moq;
using NetDaemonApps.Tests.Helpers;
using NetDaemon;
using NetDaemon.Helpers;

namespace NetDaemonApps.Tests;

public class NotificationsManagerTests
{
    private IEntities _entities;

    [Test]
    public Task AcknowledgePromptAndResetWhenPromptResponseIsYes()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        var ack = ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerAlexaResponse(new PromptResponse { EventId = "DishwasherTts", ResponseType = PromptResponseType.ResponseYes });

        ctx.VerifyCallService("input_boolean.turn_on", _entities.InputBoolean.DishwasherAck.EntityId, Times.Once());
        ctx.VerifyTextToSpeech("media_player.kitchen", "Thanks", Times.Once());
        return Task.CompletedTask;
    }

    [Test]
    public Task AcknowledgePromptWhenPromptResponseIsNo()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerAlexaResponse(new PromptResponse { EventId = "DishwasherTts", ResponseType = PromptResponseType.ResponseNo });

        ctx.VerifyTextToSpeech("media_player.kitchen", "Ok", Times.Once());

        return Task.CompletedTask;
    }

    [Test]
    public Task AnnounceWhenAnnouncedMoreThan60MinutesAgo()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "on");
        var motion = ctx.GetEntity<BinarySensorEntity>(_entities.BinarySensor.KitchenMotion.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerStateChange(motion, "on");

        ctx.Scheduler.AdvanceBy(TimeSpan.FromMinutes(61).Ticks);

        ctx.TriggerStateChange(motion, "off");
        ctx.TriggerStateChange(motion, "on");

        ctx.VerifyAnnounce("media_player.kitchen", "The Dishwasher is ready", Times.Exactly(2));
        return Task.CompletedTask;
    }

    [Test]
    public Task DontAnnounceWhenAnnouncedInTheLast60Minutes()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "on");
        var motion = ctx.GetEntity<BinarySensorEntity>(_entities.BinarySensor.KitchenMotion.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerStateChange(motion, "on");

        ctx.Scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);

        ctx.TriggerStateChange(motion, "off");
        ctx.TriggerStateChange(motion, "on");

        ctx.VerifyAnnounce("media_player.kitchen", "The Dishwasher is ready", Times.Once());
        return Task.CompletedTask;
    }

    [Test]
    public Task DontPromptWhenPromptedInTheLast15Minutes()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = ctx.GetEntity<BinarySensorEntity>(_entities.BinarySensor.KitchenMotion.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerStateChange(motion, "on");

        ctx.Scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);

        ctx.TriggerStateChange(motion, "off");
        ctx.TriggerStateChange(motion, "on");

        ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Once());
        return Task.CompletedTask;
    }

    [Test]
    public Task PromptWhenPromptedMoreThan15MinutesAgo()
    {
        var ctx = new AppTestContext();
        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = ctx.GetEntity<BinarySensorEntity>(_entities.BinarySensor.KitchenMotion.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerStateChange(motion, "on");

        ctx.Scheduler.AdvanceBy(TimeSpan.FromMinutes(16).Ticks);

        ctx.TriggerStateChange(motion, "off");
        ctx.TriggerStateChange(motion, "on");

        ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Exactly(2));
        return Task.CompletedTask;
    }

    [Test]
    public Task PromptWhenThereHasBeenNoPreviousPrompt()
    {
        var ctx = new AppTestContext();

        _entities = new Entities(ctx.HaContext);

        ctx.GetEntity<InputBooleanEntity>(_entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = ctx.GetEntity<BinarySensorEntity>(_entities.BinarySensor.KitchenMotion.EntityId, "off");

        ctx.InitNotificationsManager();

        ctx.TriggerStateChange(motion, "on");

        ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Once());
        return Task.CompletedTask;
    }
}

public static class NotificationsManagerTestContextInstanceExtensions
{
    public static NotificationsManager InitNotificationsManager(this AppTestContext ctx) => new(ctx.HaContext, new Entities(ctx.HaContext), ctx.AlexaMock.Object, ctx.Scheduler);
}