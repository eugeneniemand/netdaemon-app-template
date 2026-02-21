# Integration Guide: Adding Alexa DI to Program.cs

## Quick Start

Add one line to your `Program.cs` to register all Alexa services:

```csharp
builder
    .ConfigureServices(services => {
        // ... other service registrations ...
        
        // ADD THIS LINE:
        services.AddAlexaNotifications();
    })
    .Build()
    .Run();
```

That's it! All Alexa services are now registered and available for injection.

---

## Complete Program.cs Example

```csharp
using Microsoft.Extensions.DependencyInjection;
using NetDaemon.Extensions.Logging;
using NetDaemon.Runtime;
using Niemand.Helpers.Notifications;  // ← Add this using

// Create host builder
var host = NetDaemonHost
    .CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        // Register all built-in NetDaemon services
        services
            .AddNetDaemonCore()
            .AddNetDaemonRuntime()
            .AddNetDaemonCodeGenMetadata();

        // Register Alexa notification services
        services.AddAlexaNotifications();  // ← Add this line

        // Register your apps
        services.AddAppsFromAssembly(typeof(Program).Assembly);
    })
    .ConfigureLogging(loggingBuilder => loggingBuilder
        .AddNetDaemonLogging()
        .AddConsole()
        .AddDebug())
    .Build();

await host.RunAsync();
```

---

## Using in Your Apps

Once registered, inject `IAlexa` into your apps:

```csharp
[NetDaemonApp]
public class MyNotificationApp
{
    private readonly IAlexa _alexa;
    private readonly ILogger<MyNotificationApp> _logger;

    public MyNotificationApp(IAlexa alexa, ILogger<MyNotificationApp> logger)
    {
        _alexa = alexa;
        _logger = logger;

        // Subscribe to events, set up automations, etc.
    }

    public void SayHello()
    {
        _alexa.Announce("media_player.living_room", "Hello from my app!");
    }
}
```

---

## Advanced Configuration

If you need custom behavior, use the overload with options:

```csharp
services.AddAlexaNotifications(options => {
    // Customize volume settings
    options.VolumeManagerFactory = (ha, svcs, entities, devices) => {
        var manager = new VolumeManager(ha, svcs, entities, devices);
        // Add custom logic if needed
        return manager;
    };

    // Customize message formatting
    options.MessageFormatterFactory = (provider) => {
        return new MessageFormatter();
        // Could add custom effects here in the future
    };
});
```

---

## Dependency Graph

Here's how the DI container wires everything together:

```
Program.cs
  └── services.AddAlexaNotifications()
      ├── Registers: Subject<PromptResponse> (Singleton)
      ├── Registers: MessageFormatter (Singleton)
      ├── Registers: VolumeManager (Scoped)
      │   ← Depends on: IHaContext, IServices, IEntities
      ├── Registers: NotificationProcessor (Scoped)
      │   ← Depends on: IEntities, IScheduler, VolumeManager, MessageFormatter
      ├── Registers: PromptResponseHandler (Scoped)
      │   ← Depends on: Subject<PromptResponse>, IAppConfig<AlexaConfig>, ILogger
      └── Registers: IAlexa → Alexa (Scoped)
          ← Depends on: All of the above + more

Your App
  └── Requests: IAlexa
      └── DI Container provides: Fully-configured Alexa instance
```

---

## No Breaking Changes

✅ This integration is completely backwards compatible:
- Existing code continues to work without changes
- `IAlexa` interface is unchanged
- All public methods work the same way
- Configuration format unchanged

---

## Testing Your Integration

```csharp
[Test]
public void When_ServiceRegistered_Then_CanResolveAlexa()
{
    // Arrange
    var services = new ServiceCollection();
    services.AddAlexaNotifications();
    var provider = services.BuildServiceProvider();

    // Act
    var alexa = provider.GetRequiredService<IAlexa>();

    // Assert
    Assert.IsNotNull(alexa);
}
```

---

## Troubleshooting

### Issue: "Unable to resolve service for IHaContext"
**Solution:** Make sure NetDaemon services are registered before Alexa:
```csharp
services.AddNetDaemonCore();  // ← Must come first
services.AddAlexaNotifications();  // ← Then Alexa
```

### Issue: "Type not found in namespace"
**Solution:** Add the using statement:
```csharp
using Niemand.Helpers.Notifications;
```

### Issue: Multiple instances of Alexa being created
**Solution:** Ensure `IAppConfig<AlexaConfig>` is registered as a singleton in your base configuration.

---

## Next: Running Tests

Once integrated, run your test suite to verify everything works:

```bash
dotnet test ./Niemand.Tests/Niemand.Tests.csproj
```

All existing tests should pass without modification due to backwards compatibility.

---

## Summary

| Step | Action |
|------|--------|
| 1 | Add `using Niemand.Helpers.Notifications;` to Program.cs |
| 2 | Add `services.AddAlexaNotifications();` in ConfigureServices |
| 3 | Inject `IAlexa` into your apps via constructor |
| 4 | Run tests to verify - Done! ✅ |

That's all! The DI integration is complete and ready to use.
