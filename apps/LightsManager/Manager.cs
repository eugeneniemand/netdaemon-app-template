using System;
using System.Linq;
using NetDaemon.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class Manager
    {
        private readonly INetDaemonRxApp _app;
        private          IDisposable?    _timer;
        private readonly Configurator    _configurator;

        private bool IsActive => _timer != null || _configurator.PresenceSensors.Any(e => string.Equals(e.State, "on", StringComparison.OrdinalIgnoreCase));

        public Manager(INetDaemonRxApp app, Configurator configurator)
        {
            _app          = app;
            _configurator = configurator;
            _configurator.Configure(_app);
            Subscriptions.Setup(_app, _configurator, this);
        }

        private void TurnOffControlEntities()
        {
            _configurator.Lights.ForEach(e => e.TurnOff());
            _configurator.Switches.ForEach(e => e.TurnOff());
        }

        private void TurnOnControlEntities()
        {
            _configurator.Lights.ForEach(e => e.TurnOn());
            _configurator.Switches.ForEach(e => e.TurnOn());
        }

        public void OnPresenceStarted(object? sender, HassEventArgs args)
        {
            if (_configurator.LuxAboveLimit) return;

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

            SetTimer(TimeSpan.FromSeconds(_configurator.TimeoutSeconds), () =>
            {
                TurnOffControlEntities();
                _app.LogEventHandled(args);
            }, args);
        }

        public void OnHouseModeChanged(object? sender, HassEventArgs args)
        {
            if (!IsActive)
            {
                _configurator.Configure(_app); // Configure so entities are ready for next time
                return;
            }

            TurnOffControlEntities();
            _configurator.Configure(_app);
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