using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

//[NetDaemonApp]
////[Focus]
//public class EntityManager(IHaContext haContext, Entities entities, Services services, ILogger<EntityManager> logger, IScheduler scheduler) : IAsyncInitializable, IDisposable
//{
//    private const string nd_bool = "binary_sensor.netdaemon_created_bool_2";

    
//    public async Task InitializeAsync(CancellationToken cancellationToken)
//    {
//        var boolVal = false;        
//        haContext.Entity(nd_bool).StateChanges().Subscribe(s =>
//        {
//            logger.LogInformation($"Old: {s.Old?.State}, New: {s.New?.State}");
            
//        });

//        scheduler.RunEvery(
//            TimeSpan.FromSeconds(10), 
//            DateTimeOffset.Now, 
//            () => {
//                services.NetdaemonEntities.SetState(new NetdaemonEntitiesSetStateParameters() { EntityId = nd_bool, State = !boolVal });
//                //boolVal = !boolVal;
//            }                
//        );
//    }

//    public void Dispose()
//    {
//        services.NetdaemonEntities.RemoveEntity(new NetdaemonEntitiesRemoveEntityParameters() { EntityId = nd_bool });
//    }
//}