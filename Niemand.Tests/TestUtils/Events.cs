namespace AwesomeNetdaemon.Test.TestUtils;

public static class Events
{
    public static class Light
    {
        public static object TurnOff(LightEntity entity) =>
            new
            {
                Domain  = "light",
                Service = "turn_off",
                Target = new
                {
                    EntityIds = new[] { entity.EntityId }
                },
            };
        public static object TurnOn(LightEntity entity) =>
            new
            {
                Domain  = "light",
                Service = "turn_on",
                Target = new
                {
                    EntityIds = new[] { entity.EntityId }
                },
            };
    }

    public static class InputBoolean
    {
        public static object TurnOn(InputBooleanEntity entity) =>
            new
            {
                Domain  = "input_boolean",
                Service = "turn_on",
                Target = new
                {
                    EntityIds = new[] { entity.EntityId }
                },
            };

        public static object TurnOff(InputBooleanEntity entity) =>
            new
            {
                Domain  = "input_boolean",
                Service = "turn_off",
                Target = new
                {
                    EntityIds = new[] { entity.EntityId }
                },
            };
    }
}