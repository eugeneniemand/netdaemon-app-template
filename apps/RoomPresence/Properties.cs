using System;
using System.Collections.Generic;
using System.Linq;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private string ActiveEntities => string.Join(", ", _presenceEntityIds.Union(_keepAliveEntityIds).Where(entityId => EntityState(entityId) == "on"));

        private string Expiry => DateTime.Now.AddSeconds(_timeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss");
        private bool NoPresence => string.IsNullOrEmpty(ActiveEntities);
        private bool Presence => !string.IsNullOrEmpty(ActiveEntities);
        private string KeepAliveEntities => string.Join(", ", _keepAliveEntityIds);
        private bool IsNightTime => _roomConfig.NightTimeEntityId != null && _roomConfig.NightTimeEntityStates.Contains(EntityState(_roomConfig.NightTimeEntityId));
        private int Lux => _roomConfig.LuxEntityId == null ? 0 : int.TryParse(EntityState(_roomConfig.LuxEntityId), out var luxInt) ? luxInt : 0;

        private IEnumerable<string> GetControlEntities()
        {
            LogTrace("GetControlEntities()");
            IEnumerable<string> entities = _controlEntityIds;

            LogTrace("If room state is Override then get control and night control entities to enable guard dog");
            if (RoomIs(RoomState.Override))
            {
                entities = _controlEntityIds.Union(_nightControlEntityIds);
                LogDebug("Control Entities: {entities}", entities);
                return entities;
            }

            LogTrace("Do Night Control entities exist");
            if (!_nightControlEntityIds.Any())
            {
                LogTrace("Night Control entities doesnt exists");
                entities = _controlEntityIds;
            }
            else if (IsNightTime)
            {
                LogTrace("Return Night Control Entities");
                entities = _nightControlEntityIds;
            }

            LogDebug("Control Entities: {entities}", entities);
            return entities;
        }

        private bool IsDisabled()
        {
            LogTrace("IsDisabled()");

            var switchEntity = EntityState(_roomConfig.EnabledSwitchEntityId);
            LogTrace("{switch} is {state}?", _roomConfig.EnabledSwitchEntityId, switchEntity);

            if (switchEntity != "off") return false;
            SetRoomState(RoomState.Disabled);
            return true;
        }

        private bool LuxBelowThreshold()
        {
            if (_roomConfig.LuxEntityId == null)
                return true;

            var isBelowThreshold = Lux <= LuxLimit();
            LogDebug("Lux Below Threshold: {b} ({Lux})", isBelowThreshold, Lux);
            return isBelowThreshold;
        }

        private int LuxLimit()
        {
            LogTrace("LuxLimit()");

            if (string.IsNullOrWhiteSpace(_roomConfig.LuxLimitEntityId))
            {
                var roomConfigLuxLimit = _roomConfig.LuxLimit ?? 40;
                LogTrace("Lux Limit from Config");
                LogDebug("Lux Limit: {roomConfigLuxLimit}", roomConfigLuxLimit);
                return roomConfigLuxLimit;
            }

            if (int.TryParse(_app.State(_roomConfig.LuxLimitEntityId)?.State?.ToString(), out int luxEntityVal))
            {
                LogTrace("Lux Limit from Entity");
                LogDebug("Lux Limit: {luxEntityVal}", luxEntityVal);
                return luxEntityVal;
            }

            LogTrace($"Could Not Parse {_roomConfig.LuxLimitEntityId} roomState");
            return 1000;
        }

        private bool RoomIs(RoomState roomState)
        {
            LogTrace("Is RoomState: {s}?", roomState.ToString().ToLower());

            string roomEntityState = EntityState(_roomConfig.RoomPresenceEntityId);

            LogTrace("RoomState is: {roomState}", roomEntityState);
            return
                roomEntityState == roomState.ToString().ToLower();
        }

        private double CalculateFractionBasedOnIntervalAndElapsedTime(DateTime startTime, DateTime endTime, int interval, int startValue, int endValue)
        {
            var minutes = (endTime - startTime).TotalMinutes;
            var fraction = minutes / interval;
            var range = endValue - startValue;
            var inc = range / fraction;

            if (Now < startTime || Now > endTime) return startValue;

            var minutesElapsed = (Now - startTime).TotalMinutes;
            var intervalsElapsed = minutesElapsed / interval;
            return startValue + (intervalsElapsed * inc);
        }
    }
}
