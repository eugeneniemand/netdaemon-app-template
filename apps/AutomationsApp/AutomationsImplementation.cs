using System;
using System.Linq;
using System.Reactive.Concurrency;
using NetDaemon.Common.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace AutomationsApp
{
    public class AutomationsImplementation
    {
        private readonly INetDaemonRxApp _app;

        private IScheduler? Scheduler { get; }

        public AutomationsImplementation(INetDaemonRxApp app, IScheduler? scheduler = null)
        {
            Scheduler = scheduler;
            _app      = app;
        }

        public void Initialize()
        {
            _app.RunIn(TimeSpan.FromSeconds(1), () =>
            {
                var ls      = _app.EntityIds.Where(e => e.EndsWith("last_seen"));
                var missing = _app.States.Where(s => ls.Contains(s.EntityId)).Where(s => s.State != "unknown").Where(s => DateTime.Parse($"{s.State}").Date != DateTime.Today);
            });
        }
    }
}