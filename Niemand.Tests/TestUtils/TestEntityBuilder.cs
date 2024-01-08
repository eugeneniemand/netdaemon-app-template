using NetDaemon.Client.HomeAssistant.Model;
using NetDaemon.HassModel;

namespace AwesomeNetdaemon.Test.TestUtils;

public static class TestEntityBuilder
{
    public static NumericSensorEntity CreateNumericEntity(this IHaContext haContext, string entityId) => new(haContext, entityId);
    public static LightEntity CreateLightEntity(this IHaContext haContext, string entityId) => new(haContext, entityId);
    public static BinarySensorEntity CreateBinarySensorEntity(this IHaContext haContext, string entityId) => new(haContext, entityId);
    public static PersonEntity CreatePersonEntity(this IHaContext haContext, string entityId) => new(haContext, entityId);
}