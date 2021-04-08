using System;
using System.Globalization;
using System.Linq;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        public void HandleEvent()
        {
            LogTrace("HandleEvent()");

            if (IsDisabled() || !LuxBelowThreshold()) return;

            SetRoomState(RoomState.Active);
        }

        private void HandleTimer()
        {
            LogTrace("Is there Presence?");
            if (NoPresence)
            {
                LogTrace("Presence: {p}", false);
                SetRoomState(RoomState.Idle);
            }
            else
            {
                LogTrace("Presence: {p}", true);
                SetRoomState(RoomState.Active);
            }
        }

        private void NightModeChanged()
        {
            _app.Delay(TimeSpan.FromSeconds(1));
            if (IsNightTime)
            {
                foreach (var entityId in _controlEntityIds.Except(_nightControlEntityIds))
                    _app.Entity(entityId).TurnOff();
                foreach (var entityId in _nightControlEntityIds) _app.Entity(entityId).TurnOn();
            }
            else
            {
                foreach (var entityId in _nightControlEntityIds.Except(_controlEntityIds))
                    _app.Entity(entityId).TurnOff();
                foreach (var entityId in _controlEntityIds) _app.Entity(entityId).TurnOn();
            }
        }

        private void DisableCircadian()
        {
            if (_roomConfig.CircadianSwitchEntityId == null) return;
            _app.Entity(_roomConfig.CircadianSwitchEntityId).TurnOff();
        }

        private void EnableCircadian()
        {
            if (_roomConfig.CircadianSwitchEntityId == null) return;
            _app.Entity(_roomConfig.CircadianSwitchEntityId).TurnOn();
        }

        private void TurnOffControlEntities()
        {
            LogTrace("TurnOffControlEntities()");
            foreach (var entityId in GetControlEntities())
            {
                LogDebug("Turn Off Control Entity: {entityId}", entityId);
                _app.Entity(entityId).TurnOff();
            }
        }

        private void TurnOnControlEntities()
        {
            LogTrace("TurnOnControlEntities()");
            foreach (var entityId in GetControlEntities())
                if (_app.State(entityId)?.State == "off")
                {
                    LogDebug("Turn On Control Entity: {entityId}", entityId);

                    if (_roomConfig.SunriseEnabled)
                    {
                        _app.Entity(entityId).TurnOn(new { brightness = _roomConfig.SunriseStartBrightness });
                        _app.Delay(TimeSpan.FromMilliseconds(200));
                        _app.Entity(entityId).TurnOn(new { kelvin = _roomConfig.SunriseStartKelvin });

                        _brightnessTimer = _app.RunEvery(TimeSpan.FromMinutes(_roomConfig.SunriseUpdateInterval), () =>
                        {
                            UpdateBrightness(entityId);
                            UpdateColour(entityId);
                        });
                    }
                    else
                    {
                        _app.Entity(entityId).TurnOn();
                    }
                }
                else
                {
                    LogDebug("Entity already on: {entityId}", entityId);
                }
        }


        private void UpdateBrightness(string entityId)
        {
            if (_app.State(entityId)?.State == "off" || !_roomConfig.SunriseBrightnessEnabled) return;
            var startTime = DateTime.ParseExact(_roomConfig.SunriseStartTime, "HH:mm", CultureInfo.InvariantCulture);
            var endTime   = DateTime.ParseExact(_roomConfig.SunriseEndTime, "HH:mm", CultureInfo.InvariantCulture);
            var brightness = CalculateFractionBasedOnIntervalAndElapsedTime(startTime, endTime,
                _roomConfig.SunriseUpdateInterval, _roomConfig.SunriseStartBrightness,
                _roomConfig.SunriseEndBrightness);
            _app.Entity(entityId).TurnOn(new { brightness });
        }

        private void UpdateColour(string entityId)
        {
            if (_app.State(entityId)?.State == "off" || !_roomConfig.SunriseColourEnabled) return;
            var startTime = DateTime.ParseExact(_roomConfig.SunriseStartTime, "HH:mm", CultureInfo.InvariantCulture);
            var endTime   = DateTime.ParseExact(_roomConfig.SunriseEndTime, "HH:mm", CultureInfo.InvariantCulture);
            var kelvin = CalculateFractionBasedOnIntervalAndElapsedTime(startTime, endTime,
                _roomConfig.SunriseUpdateInterval, _roomConfig.SunriseStartKelvin, _roomConfig.SunriseEndKelvin);
            _app.Entity(entityId).TurnOn(new { kelvin });
        }
    }
}