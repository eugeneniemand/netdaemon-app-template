// namespace Niemand;
//
// [NetDaemonApp]
// //[Focus]
// public class Alarm
// {
//     public Alarm(IEntities entities, ILogger<Alarm> logger)
//     {
//         var doors = new[]
//         {
//             entities.BinarySensor.BackDoor,
//             entities.BinarySensor.FrontDoor,
//             entities.BinarySensor.DiningDoor,
//             entities.BinarySensor.GarageBackDoor,
//             entities.BinarySensor.LoungeDoor
//         };
//
//         doors.StateChanges()
//              .Where(change => change.New.IsOn())
//              .Subscribe(change =>
//              {
//                  logger.LogDebug("Door Opened: {door}", change.Entity.EntityId);
//                  entities.Switch.AlarmBeepTwo.TurnOn();
//              });
//     }
// }