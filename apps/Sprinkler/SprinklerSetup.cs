using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Timers;
using NetDaemon.Common.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Sprinkler
{
    public class SprinklerSetup : NetDaemonRxApp
    {
        public override void Initialize()
        {
            var app = new SprinklerApp(this);
            app.Initialize();
        }
    }

    public class SprinklerApp 
    {
        private readonly INetDaemonRxApp _app;
        public int _zone = 1;
        private int _selectedZone;
        private int _switchDelay;

        public SprinklerApp(INetDaemonRxApp app)
        {
            _app = app;
        }

        public void Initialize()
        {
            _zone = !string.IsNullOrEmpty( _app.State("sensor.zone")?.State) ? (int) int.Parse(_app.State("sensor.zone")?.State) : 1;
            
            _app.Entity("input_number.selected_zone")
                .StateChanges
                .Where(s => s.New.State != "0")
                .Subscribe(s => { TurnOnSelectedZone(); });
        }

        private void TurnOnSelectedZone()
        {
            _switchDelay = !string.IsNullOrEmpty(_app.State("input_number.switch_delay_ms")?.State) ? (int)int.Parse(_app.State("input_number.switch_delay_ms")?.State) : 1;
            _selectedZone = !string.IsNullOrEmpty(_app.State("input_number.selected_zone")?.State) ? (int)int.Parse(_app.State("input_number.selected_zone")?.State) : 1;

            while (_zone != _selectedZone)
            {
                _app.Entity("switch.Sprinkler").TurnOn();
                _app.Delay(TimeSpan.FromMilliseconds(_switchDelay));
                _app.Entity("switch.Sprinkler").TurnOff();
                _zone++;
                if (_zone > 6)
                    _zone = 1;
            }
            _app.Entity("switch.Sprinkler").TurnOn();
            _app.SetState("sensor.zone", _zone.ToString(), null);
            _app.RunIn(GetZoneDuration(_zone),() => _app.Entity("switch.Sprinkler").TurnOff());
        }

        private TimeSpan GetZoneDuration(int zone)
        {
            if (_app.States.All(e => e.EntityId != $"input_number.zone_{zone}_duration_minutes")) return TimeSpan.Zero;
            var zoneDuration = !string.IsNullOrEmpty(_app.State($"input_number.zone_{zone}_duration_minutes")?.State) ? (int)int.Parse(_app.State($"input_number.zone_{zone}_duration_minutes")?.State) : 1;
            return  TimeSpan.FromMinutes(zoneDuration);
        }
    }

}
