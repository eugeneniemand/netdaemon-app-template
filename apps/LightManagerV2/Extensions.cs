using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Client;
using NetDaemon.HassModel.Common;
using NetDaemon.HassModel.Entities;

namespace daemonapp.apps.LightManagerV2;

public static class Extensions
{
    private const string NdUserId = "ef606e2918da4355ba036a019cdcc6a0";

    public static bool IsNdUserOrNull(this StateChange stateChange)
    {
        return stateChange?.New?.Context?.UserId is null or NdUserId;
    }

    //public static void CreateEntity(this IMqttClient context, IEntityDiscoveryConfig config)
    //{

    //}

    //public static void UpdateEntity(this IMqttClient context, Entity entityu)
    //{

    //}
}