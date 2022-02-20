//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Ha;
//using Ha.Daemons;
//using HomeAssistantGenerated;
//using Moq;
//using NetDaemon.Extensions.Scheduler;
//using NetDaemon.HassModel.Entities;
//using NetDaemon.HassModel.Tests.Entities;
//using Xunit;
//using Xunit.Abstractions;

//public partial class NotificationEngineTests
//{
//    private readonly ITestOutputHelper _output;

//    public NotificationEngineTests(ITestOutputHelper output)
//    {
//        _output = output;
//    }

//    [Fact]
//    public void AnnounceMessageOnEvent()
//    {
//        var ha           = new HaContextMock();
//        var mediaPlayer1 = new MediaPlayerEntity(ha.Object, "media_player.dummy");
//        var daemon       = new Mock<INotificationDaemon>();
//        var config       = new NotificationConfig(NotificationEnum.Test, "Dummy Message", new List<MediaPlayerEntity> { mediaPlayer1 });
//        daemon.SetupGet(p => p.Config).Returns(config);
//        var engine = new NotificationEngine(ha.Object, new Mock<INetDaemonScheduler>().Object, daemon.Object);

//        daemon.Raise(notificationDaemon => notificationDaemon.NotificationRaised += null, this, new NotificationEventArgs(config, Array.Empty<object>()));

//        var data = ha.ServiceCalls
//                     .ForDomain("notify")
//                     .ForService("alexa_media")
//                     .First()
//                     .Data();
//        data["message"].Should().Be("<voice name=\"Emma\">Dummy Message</voice>");
//        data["target"].Should().BeEquivalentTo(new List<string> { mediaPlayer1.EntityId });
//        data["data"].ToDictionary()["type"].Should().Be("announce");
//    }

//    [Fact]
//    public void InitWithCorrectConfigDoesNotThrow()
//    {
//        var ha        = new HaContextMock().Object;
//        var scheduler = new Mock<INetDaemonScheduler>().Object;

//        var daemon = new Mock<INotificationDaemon>();
//        daemon.SetupGet(p => p.Config).Returns(new NotificationConfig(NotificationEnum.Calendar, "", new List<MediaPlayerEntity> { new(ha, "media_player.dummy") }));
//        Action a = () => new NotificationEngine(ha, scheduler, daemon.Object);
//        a.Should().NotThrow();
//    }

//    [Fact]
//    public void InitWithNoConfigThrows()
//    {
//        var ha        = new HaContextMock().Object;
//        var scheduler = new Mock<INetDaemonScheduler>().Object;

//        var    daemon = new Mock<INotificationDaemon>();
//        Action a      = () => new NotificationEngine(ha, scheduler, daemon.Object);
//        a.Should().Throw<ArgumentException>().WithMessage("Null Notification Config (Parameter 'Config')");
//    }

//    [Fact]
//    public void InitWithNoTargetThrows()
//    {
//        var ha        = new HaContextMock().Object;
//        var scheduler = new Mock<INetDaemonScheduler>().Object;

//        var daemon = new Mock<INotificationDaemon>();
//        daemon.SetupGet(p => p.Config).Returns(new NotificationConfig(NotificationEnum.Calendar, "", new List<MediaPlayerEntity>()));
//        Action a = () => new NotificationEngine(ha, scheduler, daemon.Object);
//        a.Should().Throw<ArgumentException>().WithMessage("Target list is empty (Parameter 'Targets')");
//    }

//    [Fact]
//    public async Task LaundryDoneNotification()
//    {
//        var ha       = new HaContextMock();
//        var entities = new Entities(ha.Object);

//        var daemon = new LaundryDone(entities);
//        await daemon.Initialise();

//        using var daemonMonitor = daemon.Monitor();

//        var entity = new Entity(ha.Object, entities.BinarySensor.Kitchen.EntityId);
//        ha.StateAllChangeSubject.OnNext(new StateChange(entity,
//            new EntityState { State = "off" },
//            new EntityState { State = "on" }));


//        daemonMonitor.Should().Raise("NotificationRaised").WithArgs<NotificationEventArgs>(args => args.MessageParams == Array.Empty<object>());

//        var data = ha.ServiceCalls
//                     .ForDomain("notify")
//                     .ForService("alexa_media")
//                     .First()
//                     .Data();
//        data["message"].Should().Be("<voice name=\"Emma\">Dummy Message</voice>");
//        //data["target"].Should().BeEquivalentTo(new List<string> { mediaPlayer1.EntityId });
//        data["data"].ToDictionary()["type"].Should().Be("announce");
//    }

//    [Fact]
//    public void VolumeSetForAnnouncement()
//    {
//        var ha = new HaContextMock();
//        ha.Setup(h => h.GetState("input_select.house_mode")).Returns(new EntityState { State = "day" });

//        var mediaPlayer1 = new MediaPlayerEntity(ha.Object, "media_player.dummy");
//        var mediaPlayer2 = new MediaPlayerEntity(ha.Object, "media_player.dummy2");

//        var config = new NotificationConfig(NotificationEnum.Test, "Dummy Message", new List<MediaPlayerEntity> { mediaPlayer1, mediaPlayer2 });
//        var daemon = new Mock<INotificationDaemon>();
//        daemon.SetupGet(p => p.Config).Returns(config);

//        var engine = new NotificationEngine(ha.Object, new Mock<INetDaemonScheduler>().Object, daemon.Object);

//        daemon.Raise(notificationDaemon => notificationDaemon.NotificationRaised += null, this, new NotificationEventArgs(config));

//        var serviceCalls = ha.ServiceCalls.ForDomain("media_player").ForService("volume_set").ToList();

//        serviceCalls.EntityIds()
//                    .Should()
//                    .BeEquivalentTo(new List<string>
//                    {
//                        mediaPlayer1.EntityId,
//                        mediaPlayer2.EntityId
//                    });

//        serviceCalls
//            .Data()
//            .Select(d => d["volume_level"])
//            .Distinct()
//            .Should()
//            .BeEquivalentTo(new List<double> { 0.5 });
//    }

//    [Fact]
//    public void VolumeSetForWhisperAnnouncement()
//    {
//        var ha = new HaContextMock();
//        ha.Setup(h => h.GetState("input_select.house_mode")).Returns(new EntityState { State = "night" });

//        var mediaPlayer1 = new MediaPlayerEntity(ha.Object, "media_player.dummy");
//        var mediaPlayer2 = new MediaPlayerEntity(ha.Object, "media_player.dummy2");

//        var config = new NotificationConfig(NotificationEnum.Test, "Dummy Message", new List<MediaPlayerEntity> { mediaPlayer1, mediaPlayer2 });
//        var daemon = new Mock<INotificationDaemon>();
//        daemon.SetupGet(p => p.Config).Returns(config);

//        var engine = new NotificationEngine(ha.Object, new Mock<INetDaemonScheduler>().Object, daemon.Object);

//        daemon.Raise(notificationDaemon => notificationDaemon.NotificationRaised += null, this, new NotificationEventArgs(config));

//        var serviceCalls = ha.ServiceCalls.ForDomain("media_player").ForService("volume_set").ToList();

//        serviceCalls.EntityIds()
//                    .Should()
//                    .BeEquivalentTo(new List<string>
//                    {
//                        mediaPlayer1.EntityId,
//                        mediaPlayer2.EntityId
//                    });

//        serviceCalls
//            .Select(sc => sc.Data.ToDynamic().volume_level)
//            .Distinct()
//            .Should()
//            .BeEquivalentTo(new List<double> { 0.1 });
//    }
//}

