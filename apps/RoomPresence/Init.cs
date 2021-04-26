using System;
using System.Linq;
using System.Reactive.Concurrency;
using NetDaemon.Common.Reactive;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private readonly INetDaemonRxApp _app;
        private readonly string[]        _controlEntityIds;
        private readonly string[]        _keepAliveEntityIds;
        private readonly string[]        _nightControlEntityIds;
        private readonly TimeSpan        _nightTimeout;
        private readonly TimeSpan        _normalTimeout;
        private readonly TimeSpan        _overrideTimeout;
        private readonly PresenceConfig  _presenceConfig;
        private readonly string[]        _presenceEntityIds;
        private readonly RoomConfig      _roomConfig;
        private readonly string          _tracePrefix;
        private          IDisposable?    _brightnessTimer;
        private          string          _eventEntity;


        public RoomPresenceImplementation(INetDaemonRxApp app, PresenceConfig presenceConfig)
        {
            _app            = app;
            _presenceConfig = presenceConfig;

            try
            {
                _roomConfig  = presenceConfig.RoomConfig;
                Scheduler    = presenceConfig.Scheduler;
                _tracePrefix = $"({_roomConfig.Name}) - ";
                _normalTimeout = TimeSpan.FromSeconds(presenceConfig.RoomConfig.Timeout != 0
                    ? presenceConfig.RoomConfig.Timeout
                    : 300);
                _nightTimeout = TimeSpan.FromSeconds(presenceConfig.RoomConfig.NightTimeout != 0
                    ? presenceConfig.RoomConfig.NightTimeout
                    : 60);
                _overrideTimeout = TimeSpan.FromSeconds(presenceConfig.RoomConfig.OverrideTimeout != 0
                    ? presenceConfig.RoomConfig.OverrideTimeout
                    : 900);
                _presenceEntityIds     = presenceConfig.RoomConfig.PresenceEntityIds.ToArray();
                _controlEntityIds      = presenceConfig.RoomConfig.ControlEntityIds.ToArray();
                _nightControlEntityIds = presenceConfig.RoomConfig.NightControlEntityIds.ToArray();
                _keepAliveEntityIds    = presenceConfig.RoomConfig.KeepAliveEntityIds.ToArray();
                NdUserId               = presenceConfig.NdUserId;
            }
            catch (Exception e)
            {
                _app.LogError(e, "Error in Constructor");
                throw;
            }
        }

        public string? NdUserId { get; }
        public DateTime Now => Scheduler?.Now.DateTime ?? DateTime.Now;
        private IScheduler? Scheduler { get; }
        private TimeSpan Timeout => IsNightTime ? _nightTimeout : _normalTimeout;
        private IDisposable? Timer { get; set; }

        public void Initialize()
        {
            try
            {
                LogTrace("Initialize Started", new
                {
                    _roomConfig,
                    Scheduler?.Now,
                    _tracePrefix,
                    _normalTimeout   = _normalTimeout.TotalSeconds,
                    _nightTimeout    = _nightTimeout.TotalSeconds,
                    _overrideTimeout = _overrideTimeout.TotalSeconds,
                    _presenceEntityIds,
                    _controlEntityIds,
                    _nightControlEntityIds,
                    _keepAliveEntityIds,
                    NdUserId
                });
                VerifyConfig(_roomConfig);
                SetupEnabledSwitchEntity();
                IsDisabled();
                SetupSubscriptions();
                StartGuardDog();
                LogTrace("Initialize Complete");
            }
            catch (Exception e)
            {
                _app.LogError(e, "Error in Initialize");
                throw;
            }
        }

        private void SetupEnabledSwitchEntity()
        {
            LogTrace("If {switch} does not exist create it", _roomConfig.EnabledSwitchEntityId);
            var switchEntity = _app.States.FirstOrDefault(e => e.EntityId == _roomConfig.EnabledSwitchEntityId);

            if (switchEntity != null) return;
            LogTrace("{switch} does not exist, creating and turning it on", _roomConfig.EnabledSwitchEntityId);
            _app.SetState(_roomConfig.EnabledSwitchEntityId, "on", null);
        }
    }
}