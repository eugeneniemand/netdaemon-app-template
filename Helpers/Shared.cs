namespace NetDaemon.Helpers;

public static class Shared
{
    public enum Events
    {
        Cheap3HourWindowStarted
    }


}

public class Common(IHaContext haContext, IEntities entities)
{
    public MotionEntities MotionSensors => new(entities);

    public class MotionEntities
    {
        private readonly Common _common;
        private readonly IHaRegistry _registry;
        private readonly IEntities _entities;

        public MotionEntities( IEntities entities)
        {
            _entities = entities;
        }

        public BinarySensorEntity[] Downstairs =>
        [
            _entities.BinarySensor.EntranceMotion,
            _entities.BinarySensor.OfficeMotion,            
            _entities.BinarySensor.KonnectedBackOffice,
            _entities.BinarySensor.KonnectedHallway,
            _entities.BinarySensor.SittingRoomMotion,
            _entities.BinarySensor.KonnectedSittingRoom,
            _entities.BinarySensor.KonnectedKitchen,
            _entities.BinarySensor.KitchenMotion,            
            _entities.BinarySensor.DiningMotion,
            _entities.BinarySensor.LoungeMotion,
            _entities.BinarySensor.UtilityMotion,
            _entities.BinarySensor.ToiletMotion,
        ];

        public BinarySensorEntity[] Upstairs =>
        [
            _entities.BinarySensor.LandingMotion,
            _entities.BinarySensor.KonnectedLanding,
            _entities.BinarySensor.AaronMotion,
            _entities.BinarySensor.JaydenMotion,
            _entities.BinarySensor.MasterMotion,
        ];

        public BinarySensorEntity[] All => Downstairs.Union(Upstairs).ToArray();

        public BinarySensorEntity LastUpstairs => Upstairs
            .OrderByDescending(sensor => sensor.EntityState?.LastChanged)
            .First();

        public BinarySensorEntity LastDownstairs => Downstairs
            .OrderByDescending(sensor => sensor.EntityState?.LastChanged)
            .First();

        public bool UpstairsClear => Upstairs.All(e => e.IsOff());
        public bool DownstairsClear => Downstairs.All(e => e.IsOff());

        public bool LastWasUpstairs =>
            UpstairsClear &&
            DownstairsClear &&
            LastUpstairs.EntityState?.LastChanged > LastDownstairs.EntityState?.LastChanged;

        public bool LastWasDownstairs =>
            UpstairsClear &&
            DownstairsClear &&
            LastDownstairs.EntityState?.LastChanged > LastUpstairs.EntityState?.LastChanged;
    }

}