using System;
using Microsoft.Extensions.Hosting;
using NetDaemon;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging.File;
using Serilog.Formatting.Compact;

try
{
    await Host.CreateDefaultBuilder(args)
              .UseSerilog(new LoggerConfiguration()
                          .MinimumLevel.Verbose()
                          .WriteTo.Console()
                          .WriteTo.File("logs/{Date}.log")
                          .CreateLogger())
              .UseNetDaemon()
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