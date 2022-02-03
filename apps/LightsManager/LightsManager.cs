using System;
using System.Collections.Generic;
using System.Linq;
using Ha;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetDaemon.Common;
using NetDaemon.HassModel.Common;
using NetDaemon.Common.Reactive;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.HassModel.Common;


namespace LightManager;

[NetDaemonApp]
public class LightsManager : IInitializable
{
    private readonly IHaContext             _ha;
    private readonly ILogger<LightsManager> _logger;
    private readonly INetDaemonScheduler    _scheduler;
    public IEnumerable<LightsManagerConfig>? Rooms { get; set; }
    public string NdUserId { get; set; }
    public int GuardTimeout { get; set; } = 900;
    public int MinDuration { get; set; }
    public int MaxDuration { get; set; }
    public const string UNKNOWN = "Unknown";

    public LightsManager(IHaContext ha, ILogger<LightsManager> logger, INetDaemonScheduler scheduler)
    {
        _ha        = ha;
        _logger    = logger;
        _scheduler = scheduler;
    }

    public void Initialize()
    {
        var configs = Rooms.Any(r => r.Debug) ? Rooms.Where(r => r.Debug).ToList() : Rooms.ToList();
        foreach (var config in configs)
        {
            config.NdUserId     = NdUserId;
            config.GuardTimeout = GuardTimeout;

            SetupEnabledSwitchEntity(config);
            var manager = new Manager(_ha, _logger, _scheduler, config);
        }
    }

    private void SetupEnabledSwitchEntity(LightsManagerConfig config)
    {
        var switchEntity = _ha.GetAllEntities().FirstOrDefault(e => e.EntityId == config.EnabledSwitchEntityId);

        if (switchEntity != null) return;

        new Services(_ha).Netdaemon.EntityUpdate(config.EnabledSwitchEntityId, "on");
    }
}