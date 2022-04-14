using System;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using NetDaemon.AppModel;
using NetDaemon.Extensions.Logging;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.Extensions.Tts;
using NetDaemon.Runtime;
using Niemand.Helpers;

#pragma warning disable CA1812

try
{
    await Host.CreateDefaultBuilder(args)
              .UseNetDaemonAppSettings()
              .UseNetDaemonDefaultLogging()
              .UseNetDaemonRuntime()
              .UseNetDaemonTextToSpeech()
              .UseNetDaemonMqttEntityManagement()
              .ConfigureServices((context, services) =>
                  services
                      .AddAppsFromAssembly(Assembly.GetExecutingAssembly())
                      .AddNetDaemonStateManager()
                      .AddNetDaemonScheduler()
                      .AddGeneratedCode()
              )
              .Build()
              .RunAsync()
              .ConfigureAwait(false);
}
catch (Exception e)
{
    Console.WriteLine($"Failed to start host... {e}");
    throw;
}