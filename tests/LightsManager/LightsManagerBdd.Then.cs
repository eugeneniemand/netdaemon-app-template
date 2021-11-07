using System;
using System.Collections.Generic;
using System.Linq;
using LightsManager;
using Moq;
using Xunit;

public partial class LightsManagerTests
{
    private void ThenEventFired(Times times, IEnumerable<(object, HassEventArgs)> events)
    {
        var eventCount = events.Count();
        if (times == Times.Once()) Assert.Equal(1, eventCount);
        else if (times == Times.Never()) Assert.Equal(0, eventCount);
        else if (times == Times.AtLeastOnce()) Assert.True(eventCount >= 1);
    }

    private void ThenManagerStateEventFired(Times times, ManagerState state = default)
    {
        ThenEventFired(times,
            state == default
                ? _managerFiredEvents.Where(e => e.args.GetType() == typeof(ManagerStateEventArgs))
                : _managerFiredEvents.Where(e =>
                    e.args.GetType() == typeof(ManagerStateEventArgs) &&
                    ( (ManagerStateEventArgs) e.args ).State == state));
    }

    private void ThenManagerTimerResetEventFired(Times times)
    {
        ThenEventFired(times, _managerFiredEvents.Where(e => e.args.GetType() == typeof(ManagerTimerResetEventArgs)));
    }

    private void ThenManagerTimerSetEventFired(Times times, TimeSpan timeout)
    {
        var events = _managerFiredEvents.Where(e => e.args.GetType() == typeof(ManagerTimerSetEventArgs));
        ThenEventFired(times, events);
        if (timeout != default && events.Count() > 1) throw new NotImplementedException("Checking timeouts for more than 1 event is not implemented");
        Assert.Equal(timeout, ( (ManagerTimerSetEventArgs) events.First().args ).TimeoutSeconds);
    }

    private void ThenTheControlEntitiesAre(params string[] entityIds)
    {
        Assert.Equal(_config.ControlEntityIds.ToList().OrderBy(i => i), entityIds.ToList().OrderBy(i => i));
    }

    private void ThenTheControlEntityTurned(string state, Times times)
    {
        if (state == "on")
            VerifyEntityTurnOn(LightMyLight, times: times);
        else
            VerifyEntityTurnOff(LightMyLight, times: times);
    }

    private void ThenTheEntityTurnedOffTimes(string entityId, Times times)
    {
        VerifyEntityTurnOff(entityId, times: times);
    }

    private void ThenTheEntityTurnedOnTimes(string entityId, Times times)
    {
        VerifyEntityTurnOn(entityId, times: times);
    }

    private void ThenTheManagerStateIs(ManagerState state)
    {
        Assert.Equal(state, _manager.State);
    }

    private void ThenTheNightControlEntitiesAre(params string[] entityIds)
    {
        Assert.Equal(_config.NightControlEntityIds.ToList().OrderBy(i => i), entityIds.ToList().OrderBy(i => i));
    }

    private void ThenTheNightControlEntityTurned(string state, Times times)
    {
        if (state == "on")
            VerifyEntityTurnOn(LightMyNightLight, times: times);
        else
            VerifyEntityTurnOff(LightMyNightLight, times: times);
    }

    private void ThenThereIsNoActiveTimer()
    {
        Assert.Equal(false, _manager.HasActiveTimer);
    }

    private void ThenThereIsAnActiveTimer()
    {
        Assert.Equal(true, _manager.HasActiveTimer);
    }

    private void ThenAllControlEntitiesAreOff()
    {
        Assert.Equal(false, _manager.Configurator.AllControlEntities.Any(e => e.State == ON));
    }

    private void ThenOneOrMoreControlEntitiesAreOn()
    {
        Assert.Equal(true, _manager.Configurator.AllControlEntities.Any(e => e.State == ON));
    }
}