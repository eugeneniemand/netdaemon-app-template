using NetDaemon.Extensions.MqttEntityManager;
using Niemand.Helpers;

namespace Niemand;

public enum ButtonType
{
    ChoreApproval,
    Bonus,
    Penalty,
    Adjustment
}

public enum SensorType
{
    ChoreStatus
}

public static class KidConfig
{
    public static readonly string[] AllKidNames = ["jayden", "aaron", "gabriel"];

    // Pattern templates where {0} is replaced with kid name (lowercase)
    // Add more patterns here to easily extend functionality
    private static readonly string[] ButtonPrefixPatterns =
    [
        "button.{0}_choreops"        
    ];

    // Pattern templates where {0} is replaced with kid name (lowercase)
    // Add more patterns here to easily extend functionality
    private static readonly string[] SensorPrefixPatterns =
    [
        "sensor.{0}_choreops"
    ];

    private const string HelperSensorPattern = "sensor.{0}_choreops_ui_dashboard_helper";

    public static string[] GetButtonPrefixes(string kidName) =>
        ButtonPrefixPatterns.Select(p => string.Format(p, kidName)).ToArray();

    public static string[] GetSensorPrefixes(string kidName) =>
        SensorPrefixPatterns.Select(p => string.Format(p, kidName)).ToArray();

    public static string GetHelperSensorEntityId(string kidName) =>
        string.Format(HelperSensorPattern, kidName);
}

//[Focus]
[NetDaemonApp]
public class KidsChoresManager : IAsyncInitializable, IDisposable
{
    private readonly Entities _entities;
    private readonly IMqttEntityManager _entityManager;
    private readonly TimerManager _timerManager;
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly IHaContext _haContext;
    private readonly ILogger<KidsChoresManager> _logger;
    private readonly string _switchDisciplineManagerEnabled = "switch.discipline_manager_enabled";

    public SwitchEntity DisciplineManagerSwitch;

    private bool IsNdUserOrHa(StateChange stateChange) => Shared.Parents.ContainsKey(stateChange.New?.Context?.UserId ?? "Netdaemon");

    public KidsChoresManager(IHaContext haContext, IMqttEntityManager entityManager, TimerManager timerManager, IScheduler scheduler, IAlexa alexa, ILogger<KidsChoresManager> logger)
    {
        _haContext = haContext;
        _entityManager = entityManager;
        _timerManager = timerManager;
        _scheduler = scheduler;
        _alexa = alexa;
        _logger = logger;
        _entities = new Entities(haContext);
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        // TODO implement rewardApprovalButtonsPatterns handling

        SubscribeToButtonType(ButtonType.ChoreApproval);
        SubscribeToButtonType(ButtonType.Bonus);
        SubscribeToButtonType(ButtonType.Penalty);
        SubscribeToButtonType(ButtonType.Adjustment);

        SubscribeToSensorType(SensorType.ChoreStatus);
    }

    // This is a helper method to map the button entity id, for ex button.kc_aaron_chore_approval_dishawasher, to the corresponding dashboard helper sensor entity id should resolve to sensor.kc_aaron_ui_dashboard_helper
    private string GetUiHelperEntityId(string entityId)
    {
        var kidName = KidConfig.AllKidNames.FirstOrDefault(name =>
            KidConfig.GetButtonPrefixes(name).Any(prefix => entityId.StartsWith(prefix)));

        if (kidName == null)
            throw new ArgumentException($"Unknown entity id pattern: {entityId}");

        return KidConfig.GetHelperSensorEntityId(kidName);
    }

    private void SubscribeToButtonType(ButtonType buttonType)
    {
        var patterns = GetButtonPatternsForType(buttonType);
        _haContext
            .GetAllEntities()
            .Where(e => patterns.Any(pattern => e.EntityId.StartsWith(pattern)))
            .StateChanges()
            .Subscribe(c => HandleButtonPress(c, buttonType));
    }

    private void SubscribeToSensorType(SensorType sensorType)
    {
        var patterns = GetSensorPatternsForType(sensorType);

        // For chore status, subscribe to all state changes, throttle per entity, then batch
        if (sensorType == SensorType.ChoreStatus)
        {
            _haContext
                .StateChanges()
                .Where(c => patterns.Any(pattern => c.Entity.EntityId.StartsWith(pattern)))
                .Where(c => c.New?.State == "due" || c.New?.State == "overdue")
                .GroupBy(c => c.Entity.EntityId)
                .SelectMany(group => group.Throttle(TimeSpan.FromSeconds(3), _scheduler))
                .Buffer(TimeSpan.FromSeconds(10), _scheduler)
                .Where(changes => changes.Count > 0)
                .Subscribe(changes => HandleBatchedChoreStatus(changes.ToList()));
        }
    }

    private string[] GetButtonPatternsForType(ButtonType buttonType)
    {
        var suffixes = buttonType switch
        {
            ButtonType.ChoreApproval => ["_approve_chore"],
            ButtonType.Bonus => ["_apply_bonus"],
            ButtonType.Penalty => ["_apply_penalty"],
            ButtonType.Adjustment => ["_increment", "_decrement"],
            _ => Array.Empty<string>()
        };

        return BuildButtonPatterns(suffixes);
    }

    private string[] GetSensorPatternsForType(SensorType sensorType)
    {
        var suffixes = sensorType switch
        {
            SensorType.ChoreStatus => ["_chore_status"],
            _ => Array.Empty<string>()
        };

        return BuildSensorPatterns(suffixes);
    }

    /// <summary>
    /// Builds a collection of button pattern strings by combining kid names, button prefixes, and suffixes.
    /// </summary>
    /// <param name="suffixes">An array of suffixes to append to each prefix (e.g., "_on", "_off", "_long_press").</param>
    /// <returns>
    /// A string array containing all permutations of button patterns in the format: 
    /// "{prefix}{suffix}" for each kid name and corresponding prefix.
    /// </returns>
    /// <remarks>
    /// This method generates button pattern identifiers used for matching Home Assistant button entity names
    /// or automation triggers across all configured children and their associated button prefixes.
    /// Example: If kid names are ["Alice", "Bob"], prefixes for Alice are ["button.alice_chore1"], 
    /// and suffixes are ["_on", "_short"], the result would include 
    /// ["button.alice_chore1_on", "button.alice_chore1_short", ...].
    /// </remarks>
    private string[] BuildButtonPatterns(string[] suffixes) =>
    KidConfig.AllKidNames
        // Flatten each kid name to their associated button prefixes
        .SelectMany(name => KidConfig.GetButtonPrefixes(name)
            // Flatten each prefix to all possible suffix combinations
            .SelectMany(prefix => suffixes.Select(suffix => prefix + suffix)))
        .ToArray();

    /// <summary>
    /// Builds a collection of sensor pattern strings by combining kid names, sensor prefixes, and suffixes.
    /// </summary>
    /// <param name="suffixes">An array of suffixes to append to each prefix (e.g., "_chore_status").</param>
    /// <returns>
    /// A string array containing all permutations of sensor patterns in the format: 
    /// "{prefix}{suffix}" for each kid name and corresponding sensor prefix.
    /// </returns>
    /// <remarks>
    /// This method generates sensor pattern identifiers used for matching Home Assistant sensor entity names.
    /// Sensors are matched only by the sensor prefix pattern from KidConfig, combined with the suffix.
    /// Example: If kid names are ["Alice", "Bob"], sensor prefixes are ["sensor.alice_choreops"], 
    /// and suffixes are ["_chore_status"], the result would include 
    /// ["sensor.alice_choreops_chore_status", "sensor.bob_choreops_chore_status"].
    /// </remarks>
    private string[] BuildSensorPatterns(string[] suffixes) =>
    KidConfig.AllKidNames
        // Flatten each kid name to their associated sensor prefixes (filter to sensor. prefix only)
        .SelectMany(name => KidConfig.GetSensorPrefixes(name)
            // Flatten each prefix to all possible suffix combinations
            .SelectMany(prefix => suffixes.Select(suffix => prefix + suffix)))
        .ToArray();

    private void HandleButtonPress(StateChange c, ButtonType buttonType)
    {
        if (!IsNdUserOrHa(c)) { return; }

        var uiHelperEntityId = GetUiHelperEntityId(c.Entity.EntityId);
        var context = new DashboardHelperContext(_haContext, uiHelperEntityId, _logger);

        switch (buttonType)
        {
            case ButtonType.ChoreApproval:
                HandleChoreApproval(c, context);
                break;
            case ButtonType.Bonus:
                HandleBonusPress(c, context);
                break;
            case ButtonType.Penalty:
                HandlePenaltyPress(c, context);
                break;
            case ButtonType.Adjustment:
                HandlePointAdjustment(c, context);
                break;
        }
    }

    private void HandleBatchedChoreStatus(List<StateChange> changes)
    {
        if (_entities.InputBoolean.Announcements.IsOff())
            return;

        // Group by status (due vs overdue) but only process overdue
        var groupedByStatus = changes
            .Where(c => c.New?.State == "overdue")
            .GroupBy(c => c.New?.State);

        foreach (var statusGroup in groupedByStatus)
        {
            var status = statusGroup.Key;
            var choresByKid = new List<(string kidName, string choreName)>();

            foreach (var change in statusGroup)
            {
                var sensor = change.Entity;
                if (!sensor.Attributes.TryGetValue("user_name", out var kidName) ||
                    !sensor.Attributes.TryGetValue("chore_name", out var choreName))
                {
                    _logger.LogWarning("Kid name or chore name attribute not found for entity {EntityId}", sensor.EntityId);
                    continue;
                }

                choresByKid.Add((kidName.ToString(), choreName.ToString()));
            }

            if (choresByKid.Any())
            {
                AnnounceGroupedChoreStatus(status, choresByKid);
            }
        }
    }

    private void AnnounceGroupedChoreStatus(string status, List<(string kidName, string choreName)> choresByKid)
    {
        var mediaPlayers = _alexa.MediaPlayerEntityIdsForLabel("Downstairs")
            .Union(_alexa.MediaPlayerEntityIdsForLabel("Upstairs"))
            .ToList();

        if (!mediaPlayers.Any())
            return;

        // Group by kid
        var groupedByKid = choresByKid
            .GroupBy(x => x.kidName)
            .OrderBy(g => g.Key)
            .ToList();

        // Get unique chore names across all kids
        var allChores = choresByKid
            .Select(x => x.choreName)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        // Format kid names with " and " between them
        var kidNames = groupedByKid
            .Select(g => g.Key)
            .ToList();

        var childrenStr = kidNames.Count == 1
            ? kidNames[0]
            : string.Join(" and ", kidNames);

        var choresStr = string.Join(" and ", allChores);
        var childrenAndChores = $"{childrenStr}, your {choresStr}";

        string? announcementMessage = status switch
        {
            "due" => $"<amazon:emotion name='excited' intensity='high'>{childrenAndChores} is now due</amazon:emotion>",
            "overdue" => $"<amazon:emotion name='disappointed' intensity='high'>{childrenAndChores} is overdue and you are losing points</amazon:emotion>",
            _ => null
        };

        if (announcementMessage != null)
        {
            _alexa.Announce(new Alexa.Config()
            {
                Entities = mediaPlayers,
                VolumeLevel = 0.5,
                Message = announcementMessage,
                UseDefaultVoice = true
            });
        }
    }

    private void HandleChoreApproval(StateChange c, DashboardHelperContext context)
    {
        var sensorEntityId = c.Entity.EntityId.Replace("button", "sensor").Replace("approve_chore", "chore_status");
        var sensor = _haContext.GetState(sensorEntityId);

        if (!sensor.Attributes.TryGetValue("user_name", out var kidName))
        {
            _logger.LogWarning("Kid name attribute not found for entity {EntityId}", sensorEntityId);
            return;
        }

        if (!sensor.Attributes.TryGetValue("chore_name", out var choreName))
        {
            _logger.LogWarning("Chore name attribute not found for entity {EntityId}", sensorEntityId);
            return;
        }

        if (!sensor.Attributes.TryGetValue("default_points", out var chorePoints))
        {
            _logger.LogWarning("Default points attribute not found for entity {EntityId}", sensorEntityId);
            return;
        }

        // Handle JsonElement conversion
        double points = 0;
        if (chorePoints is JsonElement jsonElement)
        {
            points = jsonElement.GetDouble();
        }
        else if (chorePoints != null)
        {
            points = Convert.ToDouble(chorePoints);
        }

        NotifyPointsStateChanged(points, kidName.ToString(), choreName.ToString());
    }

    private void HandleBonusPress(StateChange c, DashboardHelperContext context)
    {
        var kidName = context.GetValue<string>("user_name");
        var bonusName = context.GetArrayItemValue<string>("bonuses", "eid", c.Entity.EntityId, "name");
        var bonusPoints = context.GetArrayItemValue<double>("bonuses", "eid", c.Entity.EntityId, "points");

        NotifyPointsStateChanged(bonusPoints, kidName, bonusName);
    }

    private void HandlePenaltyPress(StateChange c, DashboardHelperContext context)
    {
        var kidName = context.GetValue<string>("user_name");
        var penaltyName = context.GetArrayItemValue<string>("penalties", "eid", c.Entity.EntityId, "name");
        var penaltyPoints = context.GetArrayItemValue<double>("penalties", "eid", c.Entity.EntityId, "points");

        NotifyPointsStateChanged(penaltyPoints, kidName, penaltyName);
    }

    private void HandlePointAdjustment(StateChange c, DashboardHelperContext context)
    {
        var kidName = context.GetValue<string>("user_name");
        var pointsName = context.GetArrayItemValue<string>("points_buttons", "eid", c.Entity.EntityId, "name");
        var points = Convert.ToDouble(pointsName.Replace("Points ", ""));

        NotifyPointsStateChanged(points, kidName, "");
    }

    private class DashboardHelperContext
    {
        private readonly IHaContext _haContext;
        private readonly string _entityId;
        private readonly ILogger<KidsChoresManager> _logger;

        public DashboardHelperContext(IHaContext haContext, string entityId, ILogger<KidsChoresManager> logger)
        {
            _haContext = haContext;
            _entityId = entityId;
            _logger = logger;
        }

        public T? GetValue<T>(string path)
        {
            return GetDashboardValueInternal<T>(path, _entityId);
        }

        public T? GetArrayItemValue<T>(string arrayAttributeKey, string searchProperty, object searchValue, string returnProperty)
        {
            return GetDashboardArrayItemValueInternal<T>(arrayAttributeKey, searchProperty, searchValue, returnProperty, _entityId);
        }

        private JsonElement? GetDashboardAttribute(string attributeKey)
        {
            try
            {
                var entityState = _haContext.GetState(_entityId);
                if (entityState == null)
                {
                    _logger.LogWarning("Entity {EntityId} not found", _entityId);
                    return null;
                }

                if (!entityState.Attributes.TryGetValue(attributeKey, out var attributeValue))
                {
                    _logger.LogWarning("Attribute {AttributeKey} not found in entity {EntityId}", attributeKey, _entityId);
                    return null;
                }

                if (attributeValue is JsonElement jsonElement)
                {
                    return jsonElement;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard attribute {AttributeKey} from entity {EntityId}", attributeKey, _entityId);
                return null;
            }
        }

        private T? GetDashboardValueInternal<T>(string path, string entityId)
        {
            try
            {
                var pathParts = path.Split(['[', '.'], StringSplitOptions.RemoveEmptyEntries);
                if (pathParts.Length == 0)
                    return default;

                var attributeKey = pathParts[0];
                var jsonElement = GetDashboardAttribute(attributeKey);

                if (jsonElement == null)
                    return default;

                var current = jsonElement.Value;
                var remainingPath = path.Substring(attributeKey.Length);

                while (!string.IsNullOrEmpty(remainingPath))
                {
                    remainingPath = remainingPath.TrimStart('.');

                    if (remainingPath.StartsWith('['))
                    {
                        var closeBracketIndex = remainingPath.IndexOf(']');
                        if (closeBracketIndex == -1)
                            return default;

                        var indexStr = remainingPath.Substring(1, closeBracketIndex - 1);
                        if (!int.TryParse(indexStr, out var index))
                            return default;

                        if (current.ValueKind != JsonValueKind.Array)
                            return default;

                        var arrayElements = current.EnumerateArray().ToList();
                        if (index < 0 || index >= arrayElements.Count)
                            return default;

                        current = arrayElements[index];
                        remainingPath = remainingPath.Substring(closeBracketIndex + 1).TrimStart('.');
                    }
                    else
                    {
                        var nextDotOrBracket = remainingPath.IndexOfAny(['.', '[']);
                        var propertyName = nextDotOrBracket == -1
                            ? remainingPath
                            : remainingPath.Substring(0, nextDotOrBracket);

                        if (string.IsNullOrEmpty(propertyName))
                            return default;

                        if (current.ValueKind != JsonValueKind.Object)
                            return default;

                        if (!current.TryGetProperty(propertyName, out var nextElement))
                            return default;

                        current = nextElement;
                        remainingPath = nextDotOrBracket == -1
                            ? string.Empty
                            : remainingPath.Substring(nextDotOrBracket);
                    }
                }

                return ConvertJsonElementInternal<T>(current);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard value at path {Path}", path);
                return default;
            }
        }

        private T? GetDashboardArrayItemValueInternal<T>(
            string arrayAttributeKey,
            string searchProperty,
            object searchValue,
            string returnProperty,
            string entityId)
        {
            try
            {
                var arrayElement = GetDashboardAttribute(arrayAttributeKey);
                if (arrayElement == null || arrayElement.Value.ValueKind != JsonValueKind.Array)
                {
                    _logger.LogWarning("Attribute {AttributeKey} is not an array in entity {EntityId}", arrayAttributeKey, entityId);
                    return default;
                }

                foreach (var item in arrayElement.Value.EnumerateArray())
                {
                    if (item.ValueKind != JsonValueKind.Object)
                        continue;

                    if (!item.TryGetProperty(searchProperty, out var propElement))
                        continue;

                    var propValue = ConvertJsonElementInternal<string>(propElement);

                    if (propValue != null && propValue.Equals(searchValue))
                    {
                        if (!item.TryGetProperty(returnProperty, out var returnElement))
                        {
                            _logger.LogWarning("Return property {ReturnProperty} not found in matching array item", returnProperty);
                            return default;
                        }

                        return ConvertJsonElementInternal<T>(returnElement);
                    }
                }

                _logger.LogWarning("No array item found with {SearchProperty}={SearchValue} in {AttributeKey}",
                    searchProperty, searchValue, arrayAttributeKey);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching dashboard array {ArrayKey} for {SearchProperty}={SearchValue}",
                    arrayAttributeKey, searchProperty, searchValue);
                return default;
            }
        }

        private T? ConvertJsonElementInternal<T>(JsonElement element)
        {
            try
            {
                var targetType = typeof(T);

                // Handle nullable types
                var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

                return element.ValueKind switch
                {
                    JsonValueKind.String when underlyingType == typeof(string) => (T)(object)element.GetString()!,
                    JsonValueKind.Number when underlyingType == typeof(int) => (T)(object)element.GetInt32(),
                    JsonValueKind.Number when underlyingType == typeof(long) => (T)(object)element.GetInt64(),
                    JsonValueKind.Number when underlyingType == typeof(double) => (T)(object)element.GetDouble(),
                    JsonValueKind.Number when underlyingType == typeof(float) => (T)(object)element.GetSingle(),
                    JsonValueKind.Number when underlyingType == typeof(decimal) => (T)(object)element.GetDecimal(),
                    JsonValueKind.True when underlyingType == typeof(bool) => (T)(object)true,
                    JsonValueKind.False when underlyingType == typeof(bool) => (T)(object)false,
                    JsonValueKind.Array when underlyingType == typeof(List<>) || targetType.IsGenericType =>
                        (T)(object)element.EnumerateArray().ToList()!,
                    _ => default
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting JsonElement to {TargetType}", typeof(T).Name);
                return default;
            }
        }
    }

    private void NotifyPointsStateChanged(double points, string childName, string chore)
    {
        if (_entities.InputBoolean.Announcements.IsOff())
            return;

        var downstairs = _alexa.MediaPlayerEntityIdsForLabel("Downstairs");
        var upstairs = _alexa.MediaPlayerEntityIdsForLabel("Upstairs");
        var sfx = "";
        string message = "";
        string pointMessage = Math.Abs(points) == 1 ? "point" : "points";
        string choreMessage = chore != "" ? $"{pointMessage} for {chore}" : $"bonus {pointMessage}";

        switch (points)
        {
            case 0:
                message = $"{childName}, well done you completed, {chore}";
                sfx = "<audio src=\"soundbank://soundlibrary/cloth_leather_paper/money_coins/money_coins_03\"/>";
                break;
            case > 0:
                // format points to zero decimal integer
                message = $"<amazon:emotion name=\"excited\" intensity=\"hight\">{childName}, you gained {points:#} {choreMessage}</amazon:emotion>";
                sfx = "<audio src=\"soundbank://soundlibrary/cloth_leather_paper/money_coins/money_coins_03\"/>";
                break;
            default:
                message = $"<amazon:emotion name=\"disappointed\" intensity=\"high\">{childName}, you lost {Math.Abs(points):#} {choreMessage}</amazon:emotion>";
                sfx = "<audio src=\"soundbank://soundlibrary/telephones/pay_phones/pay_phones_05\"/>";
                break;
        }
        var media_players = downstairs.Union(upstairs).ToList();
        _alexa.TextToSpeech(new Alexa.Config() { Entities = media_players, VolumeLevel = 0.5, Message = sfx, Whisper = false });
        _alexa.TextToSpeech(new Alexa.Config() { Entities = media_players, VolumeLevel = 0.5, Message = message, Whisper = false });
    }



    public async void Dispose()
    {
        await _entityManager.RemoveAsync(_switchDisciplineManagerEnabled);
    }

    /// <summary>
    /// Compares two StateChange objects by entity ID and state to detect distinct chore status changes.
    /// </summary>
    private sealed class ChoreStateEqualityComparer : IEqualityComparer<StateChange>
    {
        public bool Equals(StateChange x, StateChange y)
        {
            if (x == null || y == null)
                return x == y;

            return x.Entity.EntityId == y.Entity.EntityId && x.New?.State == y.New?.State;
        }

        public int GetHashCode(StateChange obj)
        {
            return HashCode.Combine(obj?.Entity.EntityId, obj?.New?.State);
        }
    }
}