using System.Collections.Generic;
using System.Linq;
using NetDaemon.Common.Reactive;

namespace LightsManager
{
    public class LightEntityDummy
    {
        private readonly INetDaemonRxApp _app;
        public readonly  string          EntityId;

        public LightEntityDummy(INetDaemonRxApp app, IEnumerable<string> entityIds)
        {
            _app     = app;
            EntityId = entityIds.First();
        }

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