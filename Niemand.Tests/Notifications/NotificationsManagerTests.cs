using NetDaemon.Helpers;
using Niemand.NotificationManager;
using Niemand.Tests;

namespace NetDaemonApps.Tests;

public class NotificationsManagerTests(NotificationManagerSut sut, IEntities entities, StateChangeManager state, TestEntityBuilder entityBuilder)
{
    [Fact]
    public void AcknowledgePromptAndResetWhenPromptResponseIsYes()
    {
        var ack         = entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "off");
        var mediaPlayer = entityBuilder.CreateMediaPlayerEntity("media_player.kitchen");
        sut.Init();

        sut.Alexa.QueueResponse(new PromptResponse { EventId = "DishwasherTts", ResponseType = PromptResponseType.ResponseYes });

        state.ServiceCalls.Should().ContainEquivalentOf(Events.InputBoolean.TurnOn(ack));
        state.ServiceCalls.Should().ContainEquivalentOf(Events.Notify.AlexaMedia(mediaPlayer, "Thanks", "tts"));
    }

    [Fact]
    public void AcknowledgePromptWhenPromptResponseIsNo()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "off");
        var mediaPlayer = entityBuilder.CreateMediaPlayerEntity("media_player.kitchen");
        sut.Init();

        sut.Alexa.QueueResponse(new PromptResponse { EventId = "DishwasherTts", ResponseType = PromptResponseType.ResponseNo });

        state.ServiceCalls.Should().ContainEquivalentOf(Events.Notify.AlexaMedia(mediaPlayer, "Ok", "tts"));
    }

    [Fact]
    public void AnnounceWhenAnnouncedMoreThan60MinutesAgo()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "on");
        var mediaPlayer = entityBuilder.CreateMediaPlayerEntity("media_player.kitchen");
        var motion      = entityBuilder.CreateBinarySensorEntity(entities.BinarySensor.KitchenMotion.EntityId, "off");
        var status      = new DishwasherNotificationConfig(entities).Status;
        state.Change(status, "ready");
        sut.Init();

        state.Change(motion, "on");

        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(61).Ticks);

        state.Change(motion, "off");
        state.Change(motion, "on");
        
        state.ServiceCalls.Filter(Domain.Notify).Should().BeEquivalentTo(new[]  {
                Events.Notify.AlexaMedia(mediaPlayer, "The Dishwasher is ready", "announce"),
                Events.Notify.AlexaMedia(mediaPlayer, "The Dishwasher is ready", "announce")
            });
    }

    [Fact]
    public void DontAnnounceWhenAnnouncedInTheLast60Minutes()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "on");
        var mediaPlayer = entityBuilder.CreateMediaPlayerEntity("media_player.kitchen");
        var motion      = entityBuilder.CreateBinarySensorEntity(entities.BinarySensor.KitchenMotion.EntityId, "off");
        var status      = new DishwasherNotificationConfig(entities).Status;
        state.Change(status, "ready");
        sut.Init();

        state.Change(motion, "on");

        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);

        state.Change(motion, "off");
        state.Change(motion, "on");
        
        state.ServiceCalls.Filter(Domain.Notify).Should().BeEquivalentTo(new[]  {
            Events.Notify.AlexaMedia(mediaPlayer, "The Dishwasher is ready", "announce")
        });
    }

    [Fact]
    public void DontPromptWhenPromptedInTheLast15Minutes()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = entityBuilder.CreateBinarySensorEntity(entities.BinarySensor.KitchenMotion.EntityId, "off");

        sut.Init();

        state.Change(motion, "on");

        sut.Scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);

        state.Change(motion, "off");
        state.Change(motion, "on");

        //ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Once());
    }

    [Fact]
    public void PromptWhenPromptedMoreThan15MinutesAgo()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = entityBuilder.CreateBinarySensorEntity(entities.BinarySensor.KitchenMotion.EntityId, "off");

        sut.Init();

        state.Change(motion, "on");

        sut.Scheduler.AdvanceBy(TimeSpan.FromMinutes(16).Ticks);

        state.Change(motion, "off");
        state.Change(motion, "on");

        //ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Exactly(2));
    }

    [Fact]
    public void PromptWhenThereHasBeenNoPreviousPrompt()
    {
        entityBuilder.CreateInputBooleanEntity(entities.InputBoolean.DishwasherAck.EntityId, "off");
        var motion = entityBuilder.CreateBinarySensorEntity(entities.BinarySensor.KitchenMotion.EntityId, "off");

        sut.Init();

        state.Change(motion, "on");

        //ctx.VerifyPrompt("media_player.kitchen", "The Dishwasher is done. Has it been unpacked?", "DishwasherTts", Times.Once());
    }
}