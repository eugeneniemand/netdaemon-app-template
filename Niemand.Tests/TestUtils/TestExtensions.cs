using System.Reflection;
using System.Text.Json;
using NetDaemon.HassModel.Entities;

namespace AwesomeNetdaemon.Test.TestUtils;

public static class TestExtensions
{
    public static EntityState WithAttributes(this EntityState entityState, object attributes)
    {
        var copy = entityState with { };
        entityState.GetType().GetProperty("AttributesJson", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(copy, AsJsonElement(attributes));
        return copy;
    }

    public static JsonElement AsJsonElement(this object value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return JsonSerializer.Deserialize<JsonElement>(jsonString);
    }
}