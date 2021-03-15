using NetDaemon.Common.Reactive;
using System;
using System.Linq;
using System.Reactive.Concurrency;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private readonly INetDaemonRxApp _app;
        private readonly string[] _controlEntityIds;
        private readonly string[] _keepAliveEntityIds;
        private readonly string[] _nightControlEntityIds;
        private readonly TimeSpan _nightTimeout;
        private readonly TimeSpan _normalTimeout;
        private readonly string[] _presenceEntityIds;
        private readonly RoomConfig _roomConfig;
        private readonly string _tracePrefix;
        private IDisposable? BrightnessTimer;
        private TimeSpan _timeout => IsNightTime ? _nightTimeout : _normalTimeout;
        private TimeSpan _overrideTimeout;
        private IDisposable? Timer { get; set; }
        public DateTime Now => Scheduler?.Now.DateTime ?? DateTime.Now;
        private IScheduler? Scheduler { get; }
        public string NdUserId { get; }


        public RoomPresenceImplementation(INetDaemonRxApp app, RoomConfig roomConfig, IScheduler? scheduler = null, string ndUserId = null)
        {
            _app = app;

            try
            {
                _roomConfig = roomConfig;
                Scheduler = scheduler;
                _tracePrefix = $"({_roomConfig.Name}) - ";
                _normalTimeout = TimeSpan.FromSeconds(roomConfig.Timeout != 0 ? roomConfig.Timeout : 300);
                _nightTimeout = TimeSpan.FromSeconds(roomConfig.NightTimeout != 0 ? roomConfig.NightTimeout : 60);
                _overrideTimeout = TimeSpan.FromSeconds(roomConfig.OverrideTimeout != 0 ? roomConfig.OverrideTimeout : 900);
                _presenceEntityIds = roomConfig.PresenceEntityIds.ToArray();
                _controlEntityIds = roomConfig.ControlEntityIds.ToArray();
                _nightControlEntityIds = roomConfig.NightControlEntityIds?.ToArray() ?? Array.Empty<string>();
                _keepAliveEntityIds = roomConfig.KeepAliveEntityIds.ToArray();
                NdUserId = ndUserId;
            }
            catch (Exception e)
            {
                _app.LogError(e, "Error in Constructor");
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                LogTrace("Initialize Started");
                VerifyConfig(_roomConfig);
                LogConfig(_roomConfig);
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
