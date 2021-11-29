using System;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

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

        public List<Entity> Lights { get; set; }
        public List<Entity> Switches { get; set; }
        public List<Entity> AllControlEntities { get; set; }
        public List<BinarySensorEntity> PresenceSensors { get; set; }
        public BinarySensorEntity NightTime { get; set; }

        public LightEntityDummy Enabled { get; set; }

        private int Lux => _config.LuxEntityId == null ? 0 : int.Parse(_app.EntityState(_config.LuxEntityId));
        private int? LuxLimit => _config.LuxEntityId == null ? _config.LuxLimit : int.Parse(_app.EntityState(_config.LuxLimitEntityId));
        private bool IsNightTime => _config.NightTimeEntityId != null && _config.NightTimeEntityStates.Contains(_app.EntityState(_config.NightTimeEntityId));
        public bool IsEnabled => _app.EntityState(_config.EnabledSwitchEntityId) == "on";
        public TimeSpan OverrideTimeoutSeconds => TimeSpan.FromSeconds(_config.OverrideTimeout);

        public TimeSpan TimeoutSeconds => TimeSpan.FromSeconds(IsNightTime ? _config.NightTimeout == 0 ? 90 : _config.NightTimeout : _config.Timeout);

        public bool LuxAboveLimit => Lux >= LuxLimit;
        public string RoomName { get; set; }
        public List<BinarySensorEntity> ActivePresenceSensors => PresenceSensors.Where(e => string.Equals(e.State, "on", StringComparison.OrdinalIgnoreCase)).ToList();
        public string NdUserId => _config.NdUserId;

        public void Configure(INetDaemonRxApp app, IHaContext ha)
        {
            _app = app;

            Lights = new List<Entity>();
            foreach (var entityId in FilterControlEntities("light.")) Lights.Add(new Entity(ha,  entityId ));

            Switches = new List<Entity>();
            foreach (var entityId in FilterControlEntities("switch.")) Switches.Add(new Entity(ha, entityId));

            AllControlEntities = new List<Entity>();
            foreach (var entityId in _config.ControlEntityIds.Union(_config.NightControlEntityIds)) AllControlEntities.Add(new Entity(ha, entityId));

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