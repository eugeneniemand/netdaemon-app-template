using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;

namespace LightsManager
{
    internal class Subscriptions
    {
        public static event EventHandler<HassEventArgs> PresenceStartedHandler;
        public static event EventHandler<HassEventArgs> PresenceStoppedHandler;
        public static event EventHandler<HassEventArgs> HouseModeChangedHandler;

        public static void Setup(INetDaemonRxApp app, Configurator configurator, Manager manager)
        {
            PresenceStartedHandler  += manager.OnPresenceStarted;
            PresenceStoppedHandler  += manager.OnPresenceStopped;
            HouseModeChangedHandler += manager.OnHouseModeChanged;

            SetupSubscriptionHandler(app, EventType.PresenceStarted, PresenceStartedHandler, configurator.PresenceSensors.Select(p => p.EntityId), configurator.RoomName, e => e.Old?.State == "off" && e.New?.State == "on");
            SetupSubscriptionHandler(app, EventType.PresenceStopped, PresenceStoppedHandler, configurator.PresenceSensors.Select(p => p.EntityId), configurator.RoomName, e => e.Old?.State == "on" && e.New?.State == "off");
            SetupSubscriptionHandler(app, EventType.HouseModeChanged, HouseModeChangedHandler, configurator.NightTime.EntityId, configurator.RoomName);
        }

        private static void SetupSubscriptionHandler(INetDaemonRxApp app, EventType eventType, EventHandler<HassEventArgs> handler, IEnumerable<string> configEntityIds, string roomName, Func<(EntityState Old, EntityState New), bool> predicate = null!)
        {
            foreach (var entityId in configEntityIds)
            {
                app.LogInformation("Setting up {eventType} for entity {entityId}", eventType, entityId);
                app.Entity(entityId)
                   .StateChanges
                   .Where(predicate ?? DefaultPredicate)
                   .Subscribe(s =>
                   {
                       var args = new HassEventArgs(roomName, eventType) { EntityId = s.New.EntityId };
                       app.LogInformation("{correlationId} - Event Fired: {eventType} for {RoomName} by {EntityId} changing from {OldState} to {NewState}", args.CorrelationId, args.EventType, args.RoomName, args.EntityId, s.Old.State, s.New.State);
                       handler.Invoke(app, args);
                   });
            }
        }

        private static void SetupSubscriptionHandler(INetDaemonRxApp app, EventType eventType, EventHandler<HassEventArgs> handler, string? entityId, string roomName, Func<(EntityState Old, EntityState New), bool> predicate = null!)
        {
            if (entityId == null)
            {
                app.LogWarning("Not setting up {eventType} for {RoomName} because entity is {entityId}", eventType, roomName, entityId);
                return;
            }

            SetupSubscriptionHandler(app, eventType, handler, new List<string> { entityId }, roomName, predicate);
        }

        private static bool DefaultPredicate((EntityState Old, EntityState New) arg)
        {
            return true;
        }
    }
}