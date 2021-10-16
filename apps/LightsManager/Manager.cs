using System;
using System.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class Manager
    {
        private readonly INetDaemonRxApp     _app;
        private readonly LightsManagerConfig _config;
        private          IDisposable?        _timer;

        private bool IsActive => _timer != null || _config.PresenceSensors.Any(e => string.Equals(e.State, "on", StringComparison.OrdinalIgnoreCase));

        public Manager(INetDaemonRxApp app, LightsManagerConfig config)
        {
            _app    = app;
            _config = config;
            Initialize();
        }

        private void Initialize()
        {
            _config.Configure(_app);
            var subscriptions = new Subscriptions(_app, _config, this);
        }

        private void TurnOffControlEntities()
        {
            _config.Lights.ForEach(e => e.TurnOff());
            _config.Switches.ForEach(e => e.TurnOff());
        }

        private void TurnOnControlEntities()
        {
            _config.Lights.ForEach(e => e.TurnOn());
            _config.Switches.ForEach(e => e.TurnOn());
        }

        public void OnPresenceStarted(object? sender, HassEventArgs args)
        {
            DisposeTimer(args);
            TurnOnControlEntities();
            _app.LogEventHandled(args);
        }

        public void OnPresenceStopped(object? sender, HassEventArgs args)
        {
            if (IsActive)
            {
                _app.LogInformation("{CorrelationId} - Presence Active: {RoomName} no timer set", args.CorrelationId, args.RoomName);
                _app.LogEventHandled(args);
                return;
            }

            SetTimer(TimeSpan.FromSeconds(_config.TimeoutSeconds), () =>
            {
                TurnOffControlEntities();
                _app.LogEventHandled(args);
            }, args);
        }

        public void OnHouseModeChanged(object? sender, HassEventArgs args)
        {
            if (!IsActive)
            {
                _config.Configure(_app); // Configure so entities are ready for next time
                return;
            }

            TurnOffControlEntities();
            _config.Configure(_app);
            TurnOnControlEntities();
            _app.LogEventHandled(args);
        }

        private void SetTimer(TimeSpan ts, Action action, HassEventArgs args)
        {
            DisposeTimer(args);
            _timer = _app.RunIn(ts, action);
            _app.LogInformation("{CorrelationId} - Timer set: {RoomName} for {Timeout}", args.CorrelationId, args.RoomName, ts);
        }

        private void DisposeTimer(HassEventArgs args)
        {
            _timer?.Dispose();
            _timer = null;
            _app.LogInformation("{CorrelationId} - Timer disposed: {RoomName}", args.CorrelationId, args.RoomName);
        }
    }
}