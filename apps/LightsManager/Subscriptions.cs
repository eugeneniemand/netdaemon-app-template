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
        public event EventHandler<HassEventArgs> PresenceStartedHandler;
        public event EventHandler<HassEventArgs> PresenceStoppedHandler;
        public event EventHandler<HassEventArgs> HouseModeChangedHandler;

        public Subscriptions(INetDaemonRxApp app, LightsManagerConfig config, Manager manager)
        {
            PresenceStartedHandler  += manager.OnPresenceStarted;
            PresenceStoppedHandler  += manager.OnPresenceStopped;
            HouseModeChangedHandler += manager.OnHouseModeChanged;

            SetupSubscriptionHandler(app, EventType.PresenceStarted, PresenceStartedHandler, config.PresenceEntityIds.Union(config.KeepAliveEntityIds), config.Name, e => e.Old?.State == "off" && e.New?.State == "on");
            SetupSubscriptionHandler(app, EventType.PresenceStopped, PresenceStoppedHandler, config.PresenceEntityIds.Union(config.KeepAliveEntityIds), config.Name, e => e.Old?.State == "on" && e.New?.State == "off");
            SetupSubscriptionHandler(app, EventType.HouseModeChanged, HouseModeChangedHandler, config.NightTimeEntityId, config.Name);
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