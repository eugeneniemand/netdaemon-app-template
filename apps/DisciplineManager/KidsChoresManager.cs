using Humanizer;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.HassModel.Integration;
using Niemand.Helpers;
using Niemand.Helpers.Notifications;
using Stateless.Graph;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Niemand;

public enum ButtonType
{
    ChoreApproval,
    Bonus,
    Penalty
}

public static class KidConfig
{
    public static readonly string[] AllKidNames = { "jayden", "aaron", "gabriel" };

    // Pattern templates where {0} is replaced with kid name (lowercase)
    // Add more patterns here to easily extend functionality
    private static readonly string[] ButtonPrefixPatterns =
    {
        "button.kc_{0}",
        "button.{0}_kidschores"
    };

    private const string HelperSensorPattern = "sensor.{0}_kidschores_ui_dashboard_helper";

    public static string[] GetButtonPrefixes(string kidName) =>
        ButtonPrefixPatterns.Select(p => string.Format(p, kidName)).ToArray();

    public static string GetHelperSensorEntityId(string kidName) =>
        string.Format(HelperSensorPattern, kidName);
}

[Focus]
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

    private string[] GetButtonPatternsForType(ButtonType buttonType)
    {
        return buttonType switch
        {
            ButtonType.ChoreApproval => KidConfig.AllKidNames
                .SelectMany(name => KidConfig.GetButtonPrefixes(name)
                    .Where(p => p.Contains("kc_"))
                    .Select(p => p + "_chore_approval"))
                .ToArray(),
            ButtonType.Bonus => KidConfig.AllKidNames
                .SelectMany(name => KidConfig.GetButtonPrefixes(name)
                    .Where(p => p.Contains("kidschores"))
                    .Select(p => p + "_apply_bonus"))
                .ToArray(),
            ButtonType.Penalty => KidConfig.AllKidNames
                .SelectMany(name => KidConfig.GetButtonPrefixes(name)
                    .Where(p => p.Contains("kidschores"))
                    .Select(p => p + "_apply_penalty"))
                .ToArray(),
            _ => Array.Empty<string>()
        };
    }

    private void HandleButtonPress(StateChange c, ButtonType buttonType)
    {
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
        }
    }

    private void HandleChoreApproval(StateChange c, DashboardHelperContext context)
    {
        var sensorEntityId = c.Entity.EntityId.Replace("button", "sensor").Replace("chore_approval", "chore_status");
        var sensor = _haContext.GetState(sensorEntityId);

        if (!sensor.Attributes.TryGetValue("kid_name", out var kidName))
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

        NotifyPointsStateChanged((chorePoints != null ? Convert.ToDouble(chorePoints) : 0), kidName.ToString(), choreName.ToString());
    }

    private void HandleBonusPress(StateChange c, DashboardHelperContext context)
    {
        var kidName = context.GetValue<string>("kid_name");
        var bonusName = context.GetArrayItemValue<string>("bonuses", "eid", c.Entity.EntityId, "name");
        var bonusPoints = context.GetArrayItemValue<double>("bonuses", "eid", c.Entity.EntityId, "points");

        NotifyPointsStateChanged(bonusPoints, kidName, bonusName);
    }

    private void HandlePenaltyPress(StateChange c, DashboardHelperContext context)
    {
        var kidName = context.GetValue<string>("kid_name");
        var penaltyName = context.GetArrayItemValue<string>("penalties", "eid", c.Entity.EntityId, "name");
        var penaltyPoints = context.GetArrayItemValue<double>("penalties", "eid", c.Entity.EntityId, "points");

        NotifyPointsStateChanged(penaltyPoints, kidName, penaltyName);
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
                var pathParts = path.Split(new[] { '[', '.' }, StringSplitOptions.RemoveEmptyEntries);
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
                        var nextDotOrBracket = remainingPath.IndexOfAny(new[] { '.', '[' });
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

        if (points > 0)
        {
            // format points to zero decimal integer
            message = $"{childName}, you gained {points:#} points for {chore}";
            sfx = "<audio src=\"soundbank://soundlibrary/cloth_leather_paper/money_coins/money_coins_03\"/>";
        }
        else
        {
            message = $"{childName}, you lost {points:#} points for {chore}";
            sfx = "<audio src=\"soundbank://soundlibrary/telephones/pay_phones/pay_phones_05\"/>";
        }

        _alexa.TextToSpeech(new Alexa.Config() { Entities = downstairs, VolumeLevel = 0.5, Message = sfx, Whisper = false });
        _alexa.TextToSpeech(new Alexa.Config() { Entities = downstairs, VolumeLevel = 0.5, Message = message, Whisper = false });

        _alexa.TextToSpeech(new Alexa.Config() { Entities = upstairs, VolumeLevel = 0.5, Message = sfx, Whisper = false });
        _alexa.TextToSpeech(new Alexa.Config() { Entities = upstairs, VolumeLevel = 0.5, Message = message, Whisper = false });
    }



    public async void Dispose()
    {
        await _entityManager.RemoveAsync(_switchDisciplineManagerEnabled);
    }
}