using System.Reactive.Concurrency;
using LightManagerV2;
using Microsoft.Extensions.Logging;
using Niemand.Tests.Mocks;

namespace Niemand.Tests;

public class LightManagerTests(IHaContext ha, StateChangeManager state, ILogger<LightsManager> logger, IScheduler scheduler) : IClassFixture<Manager>
{
    private IHaContext Ha { get; } = ha;
    private ILogger<LightsManager> Logger { get; } = logger;
    private IScheduler Scheduler { get; } = scheduler;

    [Fact]
    public async Task IsHome()
    {
        var pir   = Ha.CreateBinarySensorEntity("binary_sensor.pir");
        var light = Ha.CreateLightEntity("light.lamp");
        
        var sut   = new Manager();
        await sut.Init(Logger, "", new RandomManagerMock(), Scheduler, ha, new MqttEntityManagerMock(), 10);
        
        
        state.Change(pir, "on");

        state.ServiceCalls.Should().BeEquivalentTo(new[]
        {
             Events.Light.TurnOn(light)
         });
        
    }

    //[Fact]
    //public void IsNotHome()
    //{
    //    _ = new PresenceApp(entities);

    //    state
    //        .Change(entities.Person.Test, "not_home");

    //    state.ServiceCalls.Should().BeEquivalentTo(new[]
    //    {
    //         Events.InputBoolean.TurnOff(entities.InputBoolean.Toggle)
    //     });
    //}
}