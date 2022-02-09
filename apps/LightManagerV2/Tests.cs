using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssistantGenerated;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Moq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;
using NetDaemonApps.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace daemonapp.apps.LightManagerV2;

internal class Tests
{
    private readonly AppTestContext         _ctx    = AppTestContext.New();
    private readonly ILogger<RandomManager> _logger = Substitute.For<ILogger<RandomManager>>(); //LoggerFactory.Create(builder => builder.AddFile("log.log")).CreateLogger<RandomManager>();

    [Test]
    public void TestCancellation()
    {
        var randomSwitch = _ctx.GetEntity<SwitchEntity>("switch.random");
        var rndManager   = new RandomManager(randomSwitch!, "1.00:00:00.100", "1.00:00:00.300", _logger);
        var rndStates    = new List<string> { "random_cancel" };
        var living = new List<LightEntity>
        {
            _ctx.GetEntity<LightEntity>("light.living")!
        };

        var dining = new List<LightEntity>
        {
            _ctx.GetEntity<LightEntity>("light.dining")!
        };

        rndManager.AddControlEntities(living, rndStates);
        rndManager.AddControlEntities(dining, rndStates);

        _ctx
            .ChangeStateFor("switch.random")
            .FromState("off")
            .ToState("random_cancel");

        living.First().VerifyCallServiceEntity("turn_on");
        dining.First().VerifyCallServiceEntity("turn_on");

        _ctx
            .ChangeStateFor("switch.random")
            .FromState("random_cancel")
            .ToState("off");

        living.First().VerifyCallServiceEntity("turn_off");
        dining.First().VerifyCallServiceEntity("turn_off");
    }

    [Test]
    public async Task TestDelay()
    {
        var haContextMoq = new Mock<IHaContext>();
        var randomSwitch = new SwitchEntity(haContextMoq.Object, "switch.random");
        var rndManager   = new RandomManager(randomSwitch, "0.00:00:00.100", "0.00:00:00.300", _logger);
        var rndStates    = new List<string> { "random_delay" };
        var living = new List<LightEntity>
        {
            new(haContextMoq.Object, "light.living")
        };

        var dining = new List<LightEntity>
        {
            new(haContextMoq.Object, "light.dining")
        };

        rndManager.AddControlEntities(living, rndStates);
        rndManager.AddControlEntities(dining, rndStates);
        rndManager.StartQueue(false);

        haContextMoq.Verify(h => h.CallService("light", "turn_on",
            It.Is<ServiceTarget>(s => s.EntityIds!.Single() == "light.living"),
            It.IsAny<LightTurnOnParameters>()));

        await Task.Delay(rndManager.RandomDuration + TimeSpan.FromMilliseconds(200));
        rndManager.StopQueue();

        haContextMoq.Verify(h => h.CallService("light", "turn_on",
            It.Is<ServiceTarget>(s => s.EntityIds!.Single() == "light.dining"),
            It.IsAny<LightTurnOnParameters>()));
    }


    [Test]
    public async Task TestLoop()
    {
        var haContextMoq = new Mock<IHaContext>();
        var randomSwitch = new SwitchEntity(haContextMoq.Object, "switch.random");
        var rndManager   = new RandomManager(randomSwitch, "0.00:00:00.100", "0.00:00:00.300", _logger);
        var rndStates    = new List<string> { "random_loop" };
        var living = new List<LightEntity>
        {
            new(haContextMoq.Object, "light.living")
        };

        var dining = new List<LightEntity>
        {
            new(haContextMoq.Object, "light.dining")
        };

        rndManager.AddControlEntities(living, rndStates);
        rndManager.AddControlEntities(dining, rndStates);
        _logger.LogInformation("Start Queue");
        rndManager.StartQueue();
        for (var i = 0; i < 5; i++) await Task.Delay(rndManager.RandomDuration + TimeSpan.FromMilliseconds(200));
        rndManager.StopQueue();

        haContextMoq.Verify(h => h.CallService("light", "turn_on",
            It.Is<ServiceTarget>(s => s.EntityIds!.Single() == "light.living"),
            It.IsAny<LightTurnOnParameters>()), Times.AtLeast(3));

        haContextMoq.Verify(h => h.CallService("light", "turn_on",
            It.Is<ServiceTarget>(s => s.EntityIds!.Single() == "light.dining"),
            It.IsAny<LightTurnOnParameters>()), Times.AtLeast(3));
    }
}