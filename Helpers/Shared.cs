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

    //public Entity<T>[] GetEntitiesForLabel<T>(string labelId) where T : Entity
    //{
        

    //    var entities = registry.GetLabel(labelId)?.Entities;
    //    if (entities == null)
    //    {
    //        return Array.Empty<Entity<T>>();
    //    }

    //    var result = new List<Entity<T>>();
    //    foreach (var entity in entities)
    //    {
    //        try
    //        {
    //            result.Add((Entity<T>)factory.CreateEntity(haContext, entity.EntityId));
    //        }
    //        catch (InvalidCastException)
    //        {
    //            // Handle or log the exception if necessary
    //        }
    //    }

    //    return result.ToArray();
    //}


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
            _entities.BinarySensor.LoungeMotion,
            _entities.BinarySensor.Lounge,
            _entities.BinarySensor.DiningMotion,
            _entities.BinarySensor.KitchenMotion,
            _entities.BinarySensor.Kitchen,
            _entities.BinarySensor.UtilityMotion,
            _entities.BinarySensor.ToiletMotion,
            _entities.BinarySensor.OfficeMotion,
            _entities.BinarySensor.EntranceMotion,
            _entities.BinarySensor.Hallway,
            _entities.BinarySensor.Study,
            _entities.BinarySensor.Landing,
            _entities.BinarySensor.LandingMotion,
        ];

        public BinarySensorEntity[] Upstairs =>
        [
            _entities.BinarySensor.PlayroomMotion,
            _entities.BinarySensor.AaronMotion,
            _entities.BinarySensor.JaydenMotion,
            _entities.BinarySensor.BathroomMotion,
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