using System;
using Microsoft.Extensions.Hosting;
using NetDaemon;
using Serilog;

try
{
    await Host.CreateDefaultBuilder(args)
              .UseSerilog(new LoggerConfiguration()
                          //.MinimumLevel.Verbose()
                          .WriteTo.Console()
                          .WriteTo.File("logs/{Date}.log")
                          .CreateLogger())
              .UseNetDaemon()
              .UseEnvironment("Development")
              .Build()
              .RunAsync();
}
catch (Exception e)
{
    Console.WriteLine($"Failed to start host... {e}");
}
finally
{
    NetDaemonExtensions.CleanupNetDaemon();
}