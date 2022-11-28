using System.Reflection;
using HomeAssistantGenerated.Logging;
using Microsoft.Extensions.Hosting;
using NetDaemon.Extensions.MqttEntityManager;
using NetDaemon.Runtime;

#pragma warning disable CA1812

try
{
    Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

    await Host.CreateDefaultBuilder(args)
              .UseNetDaemonAppSettings()
              .UseCustomLogging()
              .UseNetDaemonRuntime()
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