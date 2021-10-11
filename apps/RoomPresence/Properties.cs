using System;
using System.Collections.Generic;
using System.Linq;

namespace Presence
{
    public partial class RoomPresenceImplementation
    {
        private string ActiveEntities => string.Join(", ",
            _presenceEntityIds.Union(_keepAliveEntityIds).Where(entityId => EntityState(entityId) == "on"));

        private string Expiry => DateTime.Now.AddSeconds(Timeout.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss");
        private bool NoPresence => string.IsNullOrEmpty(ActiveEntities);
        private bool Presence => !string.IsNullOrEmpty(ActiveEntities);
        private string KeepAliveEntities => string.Join(", ", _keepAliveEntityIds);

        private bool IsNightTime => _roomConfig.NightTimeEntityId != null &&
                                    _roomConfig.NightTimeEntityStates.Contains(
                                        EntityState(_roomConfig.NightTimeEntityId));

        private int Lux => _roomConfig.LuxEntityId == null ? 0 :
            int.TryParse(EntityState(_roomConfig.LuxEntityId), out var luxInt) ? luxInt : 0;

        private IEnumerable<string> GetControlEntities()
        {
            IEnumerable<string> entities = _controlEntityIds;

            var roomState = RoomIs(RoomState.Override);
            if (roomState)
            {
                LogDebugJson(hassEventArgs, "Union Control Entities", data: roomState);
                entities = _controlEntityIds.Union(_nightControlEntityIds);
            }
            else if (IsNightTime)
            {
                LogVerboseJson(hassEventArgs, data: new { IsNightTime });
                if (_nightControlEntityIds.Any())
                {
                    LogDebugJson(hassEventArgs, "Night Control Entities Exists");
                    entities = _nightControlEntityIds;
                }
                else
                {
                    LogDebugJson(hassEventArgs, "No Night Control Entities Exists");
                    entities = _controlEntityIds;
                }
            }

            LogDebugJson(hassEventArgs, data: new { entities });
            return entities;
        }

        private bool ConditionMatched()
        {
            if (EntityState(_roomConfig.ConditionEntityId) == UNKNOWN) return true;
            return EntityState(_roomConfig.ConditionEntityId) == _roomConfig.ConditionEntityState;
        }

        private bool IsDisabled()
        {
            var isDisabled = EntityState(_roomConfig.EnabledSwitchEntityId) == "off";

            LogInfoJson(hassEventArgs, data: new { isDisabled });
            if (isDisabled) SetRoomState(RoomState.Disabled);

            return isDisabled;
        }

        private bool LuxBelowThreshold()
        {
            if (_roomConfig.LuxEntityId == null)
                return true;

            var isBelowThreshold = Lux <= LuxLimit();
            LogDebugJson(hassEventArgs, data: isBelowThreshold);

            return isBelowThreshold;
        }

        private int LuxLimit()
        {
            if (string.IsNullOrWhiteSpace(_roomConfig.LuxLimitEntityId))
            {
                var roomConfigLuxLimit = _roomConfig.LuxLimit ?? 40;
                LogDebugJson(hassEventArgs, "ValueFromConfig", data: new { roomConfigLuxLimit });
                return roomConfigLuxLimit;
            }

            if (int.TryParse(EntityState(_roomConfig.LuxLimitEntityId), out var luxEntityVal))
            {
                LogDebugJson(hassEventArgs, "ValueFromEntity", data: new { luxEntityVal });
                return luxEntityVal;
            }

            LogWarnJson(hassEventArgs, "Could Not Parse", data: new { _roomConfig.LuxLimitEntityId, entityValue = EntityState(_roomConfig.LuxLimitEntityId), returnValue = 1000 });
            return 1000;
        }

        public bool RoomIs(RoomState roomState)
        {
            string roomEntityState = EntityState(_roomConfig.RoomPresenceEntityId);

            LogDebugJson(hassEventArgs, data: new { roomEntityState });
            //if (roomEntityState == UNKNOWN) throw new ArgumentException();
            return
                roomEntityState == roomState.ToString().ToLower();
        }

        private double CalculateFractionBasedOnIntervalAndElapsedTime(DateTime startTime, DateTime endTime,
                                                                      int interval, int startValue, int endValue)
        {
            var minutes  = ( endTime - startTime ).TotalMinutes;
            var fraction = minutes / interval;
            var range    = endValue - startValue;
            var inc      = range / fraction;

            if (Now < startTime || Now > endTime) return startValue;

            var minutesElapsed   = ( Now - startTime ).TotalMinutes;
            var intervalsElapsed = minutesElapsed / interval;
            return startValue + intervalsElapsed * inc;
        }
    }
}