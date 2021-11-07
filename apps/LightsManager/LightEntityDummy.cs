using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;

namespace LightsManager
{
    public class LightEntityDummy : RxEntityBase
    {
        private readonly INetDaemonRxApp _app;
        public readonly  string          EntityId;

        public LightEntityDummy(INetDaemonRxApp app, IEnumerable<string> entityIds) : base(app, entityIds)
        {
            _app     = app;
            EntityId = entityIds.First();
        }

        public string State => _app.EntityState(EntityId);

        public void TurnOn()
        {
            _app.Entity(EntityId).TurnOn();
        }

        public void TurnOff()
        {
            _app.Entity(EntityId).TurnOff();
        }
    }
}