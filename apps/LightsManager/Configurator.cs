using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class Configurator
    {
        private LightsManagerConfig _config;
        private INetDaemonRxApp     _app;

        public Configurator(LightsManagerConfig config)
        {
            _config  = config;
            RoomName = _config.Name;
        }

        public List<LightEntityDummy> Lights { get; set; }
        public List<LightEntityDummy> Switches { get; set; }
        public List<LightEntityDummy> AllControlEntities { get; set; }
        public List<BinarySensorEntity> PresenceSensors { get; set; }
        public BinarySensorEntity NightTime { get; set; }

        public LightEntityDummy Enabled { get; set; }

        private int Lux => _config.LuxEntityId == null ? 0 : int.Parse(_app.EntityState(_config.LuxEntityId));
        private int? LuxLimit => _config.LuxEntityId == null ? _config.LuxLimit : int.Parse(_app.EntityState(_config.LuxLimitEntityId));
        private bool IsNightTime => _config.NightTimeEntityId != null && _config.NightTimeEntityStates.Contains(_app.EntityState(_config.NightTimeEntityId));
        public bool IsEnabled => _app.EntityState(_config.EnabledSwitchEntityId) == "on";

        public TimeSpan TimeoutSeconds => TimeSpan.FromSeconds(IsNightTime ? _config.NightTimeout == 0 ? 90 : _config.NightTimeout : _config.Timeout);
        public bool LuxAboveLimit => Lux >= LuxLimit;
        public string RoomName { get; set; }
        public List<BinarySensorEntity> ActivePresenceSensors => PresenceSensors.Where(e => string.Equals(e.State, "on", StringComparison.OrdinalIgnoreCase)).ToList();
        public string NdUserId => _config.NdUserId;

        public void Configure(INetDaemonRxApp app)
        {
            _app = app;

            Lights = new List<LightEntityDummy>();
            foreach (var entityId in FilterControlEntities("light.")) Lights.Add(new LightEntityDummy(app, new[] { entityId }));

            Switches = new List<LightEntityDummy>();
            foreach (var entityId in FilterControlEntities("switch.")) Switches.Add(new LightEntityDummy(app, new[] { entityId }));

            AllControlEntities = new List<LightEntityDummy>();
            foreach (var entityId in _config.ControlEntityIds.Union(_config.NightControlEntityIds)) AllControlEntities.Add(new LightEntityDummy(app, new[] { entityId }));

            PresenceSensors = new List<BinarySensorEntity>();
            foreach (var entityId in _config.PresenceEntityIds.Union(_config.KeepAliveEntityIds)) PresenceSensors.Add(new BinarySensorEntity(app, new[] { entityId }));
            Enabled   = new LightEntityDummy(app, new[] { _config.EnabledSwitchEntityId });
            NightTime = new BinarySensorEntity(app, new[] { _config.NightTimeEntityId });
        }

        private IEnumerable<string> FilterControlEntities(string filter)
        {
            return !IsNightTime || !_config.NightControlEntityIds.Any()
                ? _config.ControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase))
                : _config.NightControlEntityIds.Where(entityId => entityId.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }
    }
}