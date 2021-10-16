using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class LightsManagerConfig
    {
        private INetDaemonRxApp _app;

        public LightsManagerConfig()
        {
            PresenceEntityIds     = new List<string>();
            ControlEntityIds      = new List<string>();
            KeepAliveEntityIds    = new List<string>();
            NightControlEntityIds = new List<string>();
        }


        public string Name { get; set; } = null!;
        public int? LuxLimit { get; set; }
        public string? LuxEntityId { get; set; }
        public string? LuxLimitEntityId { get; set; }
        public int Timeout { get; set; }
        public int OverrideTimeout { get; set; }
        public IEnumerable<string> PresenceEntityIds { get; set; }
        public IEnumerable<string> ControlEntityIds { get; set; }
        public IEnumerable<string> KeepAliveEntityIds { get; set; }
        public IEnumerable<string> NightControlEntityIds { get; set; }
        public IEnumerable<string> NightTimeEntityStates { get; set; } = null!;
        public string? NightTimeEntityId { get; set; }
        public int NightTimeout { get; set; }
        public int NightBrightness { get; set; }
        public int NightColour { get; set; }
        public string RoomPresenceEntityId => $"sensor.room_presence_{Name.ToLower()}";
        public string EnabledSwitchEntityId => $"switch.room_presence_enabled_{Name.ToLower()}";
        public string? CircadianSwitchEntityId { get; set; }


        public bool SunriseEnabled { get; set; }
        public bool SunriseColourEnabled { get; set; }
        public bool SunriseBrightnessEnabled { get; set; }
        public int SunriseStartBrightness { get; set; }
        public int SunriseStartKelvin { get; set; }
        public string SunriseStartTime { get; set; } = null!;
        public string SunriseEndTime { get; set; } = null!;
        public int SunriseEndBrightness { get; set; }
        public int SunriseEndKelvin { get; set; }
        public int SunriseUpdateInterval { get; set; }
        public bool Debug { get; set; }
        public string ConditionEntityId { get; set; }
        public string ConditionEntityState { get; set; }
        public string RandomEntityId { get; set; }
        public IEnumerable<string> RandomStates { get; set; }


        public List<LightEntityDummy> Lights { get; set; } = new();

        //public List<LightEntity> Lights { get; set; } = new();
        public List<SwitchEntity> Switches { get; set; } = new();
        public List<BinarySensorEntity> PresenceSensors { get; set; } = new();
        public SensorEntity Lux { get; set; }


        private bool IsNightTime => NightTimeEntityId != null && NightTimeEntityStates.Contains(_app.EntityState(NightTimeEntityId));

        public int TimeoutSeconds => IsNightTime ? NightTimeout : Timeout;

        public void Configure(INetDaemonRxApp app)
        {
            _app = app;
            foreach (var entityId in FilterControlEntities("light.")) Lights.Add(new LightEntityDummy(app, new[] { entityId }));
            foreach (var entityId in FilterControlEntities("switch.")) Switches.Add(new SwitchEntity(app, new[] { entityId }));
            foreach (var entityId in PresenceEntityIds.Union(KeepAliveEntityIds)) PresenceSensors.Add(new BinarySensorEntity(app, new[] { entityId }));
            Lux = new SensorEntity(app, new[] { LuxEntityId });
        }

        private IEnumerable<string> FilterControlEntities(string filter)
        {
            return !IsNightTime || !NightControlEntityIds.Any()
                ? ControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase))
                : NightControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class LightEntityDummy
    {
        private readonly INetDaemonRxApp _app;
        private readonly string          _entityId;

        public LightEntityDummy(INetDaemonRxApp app, IEnumerable<string> entityIds)
        {
            _app      = app;
            _entityId = entityIds.First();
        }

        public void TurnOn()
        {
            _app.Entity(_entityId).TurnOn();
        }

        public void TurnOff()
        {
            _app.Entity(_entityId).TurnOff();
        }
    }
}