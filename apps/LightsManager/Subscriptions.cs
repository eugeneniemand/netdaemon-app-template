using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using NetDaemon.Common.Reactive;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;
using EntityState = NetDaemon.Common.EntityState;

namespace LightManager;

internal class Subscriptions
{
    public static event EventHandler<HassEventArgs> PresenceStarted;
    public static event EventHandler<HassEventArgs> PresenceStopped;
    public static event EventHandler<HassEventArgs> HouseModeChanged;
    public static event EventHandler<HassEventArgs> ManagerEnabledChanged;
    public static event EventHandler<HassEventArgs> ManualEntityStateChange;
    private static ILogger<LightsManager> _logger;

    public static void Setup(IHaContext app, ILogger<LightsManager> logger, Configurator configurator)
    {
        _logger = logger;
        SetupSubscriptionHandler(app, EventType.PresenceStarted, PresenceStarted, configurator.PresenceSensors.Select(p => p.EntityId), configurator.RoomName, e => e.Old?.State == "off" && e.New?.State == "on");
        SetupSubscriptionHandler(app, EventType.PresenceStopped, PresenceStopped, configurator.PresenceSensors.Select(p => p.EntityId), configurator.RoomName, e => e.Old?.State == "on" && e.New?.State == "off");
        SetupSubscriptionHandler(app, EventType.ManualOverride, ManualEntityStateChange, configurator.AllControlEntities.Select(p => p.EntityId), configurator.RoomName, e => e.New?.Context?.UserId != null && e.New?.Context?.UserId != configurator.NdUserId);
        SetupSubscriptionHandler(app, EventType.HouseModeChanged, HouseModeChanged, configurator.NightTime.EntityId, configurator.RoomName);
        SetupSubscriptionHandler(app, EventType.ManagerEnabledChanged, ManagerEnabledChanged, configurator.Enabled.EntityId, configurator.RoomName);
    }


    private static void SetupSubscriptionHandler(IHaContext app, EventType eventType, EventHandler<HassEventArgs> handler, IEnumerable<string> configEntityIds, string roomName, Func<StateChange, bool> predicate = null!)
    {
        foreach (var entityId in configEntityIds)
        {
            _logger.LogInformation("Setting up {eventType} for entity {entityId}", eventType, entityId);

            app.Entity(entityId)
               .StateChanges()
               .Where(predicate ?? DefaultPredicate)
               .Subscribe(s =>
               {
                   var args = new HassEventArgs(eventType, s) { EntityId = s.New.EntityId };
                   _logger.LogInformation("{RoomName} | {correlationId} - Event Raised: {eventType} by {EntityId} changing from {OldState} to {NewState} by UserId {User}", roomName, args.CorrelationId, eventType, entityId, s.Old.State, s.New.State, $"{s.New?.Context?.UserId}");
                   handler?.Invoke(roomName, args);
               });
        }
    }

    private static void SetupSubscriptionHandler(IHaContext app, EventType eventType, EventHandler<HassEventArgs> handler, string? entityId, string roomName, Func<StateChange, bool> predicate = null!)
    {
        if (entityId == null)
        {
            _logger.LogWarning("Not setting up {eventType} for {RoomName} because entity is {entityId}", eventType, roomName, entityId);
            return;
        }

        SetupSubscriptionHandler(app, eventType, handler, new List<string> { entityId }, roomName, predicate);
    }

    private static bool DefaultPredicate(StateChange arg)
    {
        return true;
    }
}