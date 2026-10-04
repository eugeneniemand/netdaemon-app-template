#region

using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Extensions.Scheduler;
using HomeAssistantGenerated;
using System.Linq;
using System.Reactive.Disposables;

#endregion

namespace LightManagerV2;

[Focus]
[NetDaemonApp]
public class LightsManager(IScheduler scheduler, IHaContext haContext, IServices services, IMqttEntityManager entityManager, IAppConfig<ManagerConfig> config, ILogger<LightsManager> managerLogger, Random? random = null) : IAsyncInitializable
{
    private readonly ManagerConfig _config = config.Value;
    private readonly Random _random = random ?? new Random();
    private readonly Dictionary<string, SwitchEntity> _disabledManagerSwitches = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, LightEntity> _randomizerOwnedLights = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IDisposable> _lightOffSchedules = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<IDisposable> _randomizerSubscriptions = [];
    private List<Manager> _managedRooms = [];
    private List<Manager> _randomizerRooms = [];
    private IDisposable _nextRoomSchedule = Disposable.Empty;
    private string? _lastTurnedOffRoomName;
    private int _desiredActiveRoomCount;
    private bool _randomizerActive;

    private TimeSpan RandomizerMinOnDuration => ParseTimeSpan(_config.RandomizerMinOnDuration, TimeSpan.FromMinutes(20));
    private TimeSpan RandomizerMaxOnDuration => ParseTimeSpan(_config.RandomizerMaxOnDuration, TimeSpan.FromMinutes(45));
    private TimeSpan RandomizerMinShuffleInterval => ParseTimeSpan(_config.RandomizerMinShuffleInterval, TimeSpan.FromMinutes(10));
    private TimeSpan RandomizerMaxShuffleInterval => ParseTimeSpan(_config.RandomizerMaxShuffleInterval, TimeSpan.FromMinutes(25));

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _managedRooms = _config.Rooms.Any(r => r.Debug)
            ? _config.Rooms.Where(r => r.Debug).ToList()
            : _config.Rooms.ToList();

        await Task.WhenAll(_managedRooms.Select(InitRoom));
        SubscribeRandomizerTriggers();

        // reset all light managers at startup to ensure they are in a known state
        ResetLightManagers();

        // reset all light managers at 7am and 3pm to ensure they are in a known state
        scheduler.ScheduleCron("0 7,15 * * *", ResetLightManagers);
    }

    private static TimeSpan ParseTimeSpan(string? value, TimeSpan fallback) =>
        TimeSpan.TryParse(value, out var result) ? result : fallback;

    private void SubscribeRandomizerTriggers()
    {
        var sunEntity = haContext.Entity("sun.sun");
        _randomizerSubscriptions.Add(sunEntity.StateChanges().Subscribe(_ => EvaluateRandomizerState()));
        _randomizerSubscriptions.Add(_config.RandomSwitchEntity.StateChanges().Subscribe(_ => EvaluateRandomizerState()));
        EvaluateRandomizerState();
    }

    private void EvaluateRandomizerState()
    {
        var sunState = haContext.Entity("sun.sun").State;
        var alarmState = _config.RandomSwitchEntity.State;

        if (!_config.RandomizerEnabled)
        {
            managerLogger.LogDebug("Randomizer evaluation skipped because feature is disabled. AlarmState={alarmState}, SunState={sunState}", alarmState, sunState);
            StopRandomizer("DisabledInConfig");
            return;
        }

        var eligibleRooms = GetEligibleRandomizerRooms();
        var shouldRun = IsAfterSunset() && eligibleRooms.Count > 0;

        managerLogger.LogDebug("Randomizer evaluated. ShouldRun={shouldRun}, AlarmState={alarmState}, SunState={sunState}, EligibleRoomCount={eligibleRoomCount}, EligibleRooms={eligibleRooms}",
            shouldRun,
            alarmState,
            sunState,
            eligibleRooms.Count,
            eligibleRooms.Select(r => r.Name).ToArray());

        if (!shouldRun)
        {
            StopRandomizer("ConditionsNotMet");
            return;
        }

        if (_randomizerActive && SameRoomSet(eligibleRooms, _randomizerRooms))
        {
            managerLogger.LogDebug("Randomizer state unchanged. AlarmState={alarmState}, SunState={sunState}, ActiveRoomCount={activeRoomCount}, TargetRoomCount={targetRoomCount}",
                alarmState,
                sunState,
                GetActiveRandomizerRoomCount(),
                _desiredActiveRoomCount);
            return;
        }

        if (_randomizerActive)
            StopRandomizer("EligibleRoomsChanged");

        _randomizerRooms = eligibleRooms;
        DisableEligibleManagers();
        _randomizerActive = true;
        _desiredActiveRoomCount = GetDesiredActiveRoomCount();
        managerLogger.LogInformation("Randomizer started. AlarmState={alarmState}, SunState={sunState}, EligibleRoomCount={eligibleRoomCount}, TargetRoomCount={targetRoomCount}, MinOnDuration={minOnDuration}, MaxOnDuration={maxOnDuration}, MinRoomGap={minRoomGap}, MaxRoomGap={maxRoomGap}",
            alarmState,
            sunState,
            _randomizerRooms.Count,
            _desiredActiveRoomCount,
            RandomizerMinOnDuration,
            RandomizerMaxOnDuration,
            RandomizerMinShuffleInterval,
            RandomizerMaxShuffleInterval);
        ActivateNextRandomRoom();
        ScheduleNextRoomAddition();
    }

    private bool IsAfterSunset() => string.Equals(haContext.Entity("sun.sun").State, "below_horizon", StringComparison.OrdinalIgnoreCase);

    private bool SameRoomSet(IEnumerable<Manager> left, IEnumerable<Manager> right) =>
        left.Select(r => r.Name).OrderBy(n => n).SequenceEqual(right.Select(r => r.Name).OrderBy(n => n), StringComparer.OrdinalIgnoreCase);

    private List<Manager> GetEligibleRandomizerRooms()
    {
        var alarmState = _config.RandomSwitchEntity.State;

        return _managedRooms
            .Where(r => r.RandomStates.Any(state => string.Equals(state, alarmState, StringComparison.OrdinalIgnoreCase)))
            .Where(r => GetRandomizerLights(r).Count > 0)
            .ToList();
    }

    private List<LightEntity> GetRandomizerLights(Manager room) =>
        room.ControlEntities
            .Union(room.NightControlEntities)
            .DistinctBy(light => light.EntityId)
            .ToList();

    private void DisableEligibleManagers()
    {
        foreach (var room in _randomizerRooms.Where(r => r.ManagerEnabled.IsOn()))
        {
            _disabledManagerSwitches[room.Name] = room.ManagerEnabled;
            room.ManagerEnabled.TurnOff();
            managerLogger.LogInformation("Randomizer disabled room manager. Room={room}, ManagerSwitch={managerSwitch}", room.Name, room.ManagerEnabled.EntityId);
        }
    }

    private int GetDesiredActiveRoomCount()
    {
        var minRooms = Math.Max(1, Math.Min(_config.RandomizerMinActiveRooms, _randomizerRooms.Count));
        var maxRooms = Math.Max(minRooms, Math.Min(_config.RandomizerMaxActiveRooms, _randomizerRooms.Count));
        return _randomizerRooms.Count == 1 ? 1 : _random.Next(minRooms, maxRooms + 1);
    }

    private int GetActiveRandomizerRoomCount() => _randomizerRooms.Count(IsRoomActive);

    private bool IsRoomActive(Manager room) => GetRandomizerLights(room).Any(light => _randomizerOwnedLights.ContainsKey(light.EntityId) && light.IsOn());

    private Manager? GetRoomForLight(LightEntity light) => _randomizerRooms.FirstOrDefault(room => GetRandomizerLights(room).Any(candidate => candidate.EntityId == light.EntityId));

    private void ActivateNextRandomRoom()
    {
        var activeRoomCount = GetActiveRandomizerRoomCount();
        if (!_randomizerActive || _randomizerRooms.Count == 0 || activeRoomCount >= _desiredActiveRoomCount)
        {
            managerLogger.LogDebug("Randomizer room activation skipped. Active={active}, EligibleRooms={eligibleRooms}, ActiveRoomCount={activeRoomCount}, TargetRoomCount={targetRoomCount}",
                _randomizerActive,
                _randomizerRooms.Count,
                activeRoomCount,
                _desiredActiveRoomCount);
            return;
        }

        var inactiveRooms = _randomizerRooms.Where(room => !IsRoomActive(room)).ToList();
        if (inactiveRooms.Count == 0)
            return;

        var candidateRooms = inactiveRooms;
        if (!string.IsNullOrWhiteSpace(_lastTurnedOffRoomName) && inactiveRooms.Count > 1)
        {
            var roomsExcludingLast = inactiveRooms.Where(room => !string.Equals(room.Name, _lastTurnedOffRoomName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (roomsExcludingLast.Count > 0)
                candidateRooms = roomsExcludingLast;
        }

        var room = candidateRooms.OrderBy(_ => _random.Next()).First();
        var light = GetRandomizerLights(room).OrderBy(_ => _random.Next()).FirstOrDefault();
        if (light == null)
            return;

        managerLogger.LogInformation("Randomizer selected next room. Room={room}, CandidateRoomCount={candidateRoomCount}, LastTurnedOffRoom={lastTurnedOffRoom}, ActiveRoomCount={activeRoomCount}, TargetRoomCount={targetRoomCount}, AlarmState={alarmState}",
            room.Name,
            candidateRooms.Count,
            _lastTurnedOffRoomName,
            activeRoomCount,
            _desiredActiveRoomCount,
            _config.RandomSwitchEntity.State);

        TurnOnRandomizerLight(light, room);
    }

    private void ScheduleNextRoomAddition()
    {
        _nextRoomSchedule.Dispose();
        _nextRoomSchedule = Disposable.Empty;

        var activeRoomCount = GetActiveRandomizerRoomCount();
        if (!_randomizerActive || activeRoomCount >= _desiredActiveRoomCount)
            return;

        var delay = GetRandomDuration(RandomizerMinShuffleInterval, RandomizerMaxShuffleInterval);
        var scheduledFor = scheduler.Now + delay;
        managerLogger.LogInformation("Randomizer scheduled next room activation. Delay={delay}, ScheduledFor={scheduledFor}, ActiveRoomCount={activeRoomCount}, TargetRoomCount={targetRoomCount}",
            delay,
            scheduledFor,
            activeRoomCount,
            _desiredActiveRoomCount);

        _nextRoomSchedule = scheduler.Schedule(delay, _ =>
        {
            ActivateNextRandomRoom();
            ScheduleNextRoomAddition();
        });
    }

    private TimeSpan GetRandomDuration(TimeSpan min, TimeSpan max)
    {
        if (max <= min)
            return min;

        var range = max - min;
        return min + TimeSpan.FromSeconds(_random.NextDouble() * range.TotalSeconds);
    }

    private void TurnOnRandomizerLight(LightEntity light, Manager room)
    {
        managerLogger.LogInformation("Randomizer turning on light. Room={room}, Light={light}, AlarmState={alarmState}, SunState={sunState}, BrightnessPct={brightnessPct}",
            room.Name,
            light.EntityId,
            _config.RandomSwitchEntity.State,
            haContext.Entity("sun.sun").State,
            100);

        _randomizerOwnedLights[light.EntityId] = light;

        if (light.Attributes?.SupportedColorModes?.Contains("color_temp") == true)
        {
            light.TurnOn(new LightTurnOnParameters
            {
                BrightnessPct = 100,
                ColorTempKelvin = light.Attributes.MaxColorTempKelvin,
                Transition = 0
            });
        }
        else if (light.Attributes?.SupportedColorModes?.Contains("brightness") == true)
        {
            light.TurnOn(new LightTurnOnParameters { BrightnessPct = 100 });
        }
        else
        {
            light.TurnOn();
        }

        ScheduleLightOff(light, room);
    }

    private void ScheduleLightOff(LightEntity light, Manager room)
    {
        DisposeLightOffSchedule(light.EntityId);
        var duration = GetRandomDuration(RandomizerMinOnDuration, RandomizerMaxOnDuration);
        var scheduledFor = scheduler.Now + duration;

        managerLogger.LogInformation("Randomizer scheduled light off. Room={room}, Light={light}, Duration={duration}, ScheduledOffAt={scheduledOffAt}, AlarmState={alarmState}",
            room.Name,
            light.EntityId,
            duration,
            scheduledFor,
            _config.RandomSwitchEntity.State);

        _lightOffSchedules[light.EntityId] = scheduler.Schedule(duration, _ =>
        {
            TurnOffRandomizerLight(light);

            if (_randomizerActive)
            {
                if (GetActiveRandomizerRoomCount() == 0)
                    _desiredActiveRoomCount = GetDesiredActiveRoomCount();

                ActivateNextRandomRoom();
                ScheduleNextRoomAddition();
            }
        });
    }

    private void TurnOffRandomizerLight(LightEntity light)
    {
        DisposeLightOffSchedule(light.EntityId);
        var room = GetRoomForLight(light);
        _lastTurnedOffRoomName = room?.Name;
        _randomizerOwnedLights.Remove(light.EntityId);

        if (light.IsOn())
        {
            managerLogger.LogInformation("Randomizer turning off light. Room={room}, Light={light}, AlarmState={alarmState}, SunState={sunState}, ActiveRoomCount={activeRoomCount}",
                room?.Name ?? "Unknown",
                light.EntityId,
                _config.RandomSwitchEntity.State,
                haContext.Entity("sun.sun").State,
                GetActiveRandomizerRoomCount());
            light.TurnOff();
        }
    }

    private void DisposeLightOffSchedule(string entityId)
    {
        if (_lightOffSchedules.Remove(entityId, out var schedule))
        {
            schedule.Dispose();
        }
    }

    private void StopRandomizer(string reason)
    {
        managerLogger.LogInformation("Randomizer stopping. Reason={reason}, AlarmState={alarmState}, SunState={sunState}, ActiveRoomCount={activeRoomCount}, OwnedLightCount={ownedLightCount}",
            reason,
            _config.RandomSwitchEntity.State,
            haContext.Entity("sun.sun").State,
            GetActiveRandomizerRoomCount(),
            _randomizerOwnedLights.Count);

        _nextRoomSchedule.Dispose();
        _nextRoomSchedule = Disposable.Empty;

        foreach (var entityId in _lightOffSchedules.Keys.ToList())
        {
            DisposeLightOffSchedule(entityId);
        }

        foreach (var light in _randomizerOwnedLights.Values.ToList())
        {
            if (light.IsOn())
                light.TurnOff();
        }

        foreach (var disabledSwitch in _disabledManagerSwitches.Values.Where(s => s.IsOff()))
        {
            managerLogger.LogInformation("Randomizer restoring room manager. ManagerSwitch={managerSwitch}", disabledSwitch.EntityId);
            disabledSwitch.TurnOn();
        }

        _randomizerOwnedLights.Clear();
        _disabledManagerSwitches.Clear();
        _randomizerRooms = [];
        _lastTurnedOffRoomName = null;
        _desiredActiveRoomCount = 0;
        _randomizerActive = false;
    }

    private void ResetLightManagers()
    {
        managerLogger.LogInformation("Resetting all light managers");
        haContext.GetAllEntities()
           .Where(e => e.EntityId.Contains("switch.light_manager_"))
           .ToList()
           .ForEach(e => (e as ISwitchEntityCore)?.TurnOn());        
    }

    private async Task InitRoom(Manager r)
    {
        await r.Init(managerLogger, _config.NdUserId, scheduler, haContext, services, entityManager, _config.GuardTimeout);
    }
}
