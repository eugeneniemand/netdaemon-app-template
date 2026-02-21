/**
 * ALEXA NOTIFICATION SYSTEM - DEPENDENCY INJECTION SETUP
 * 
 * This document explains how to register and use the Alexa notification services
 * with the .NET dependency injection container.
 */

// ==============================================================================
// BASIC SETUP (Recommended)
// ==============================================================================
// In Program.cs or your service configuration:

var builder = new NetDaemonHostBuilder();

builder
    .ConfigureServices(services => {
        // ... other service registrations ...
        
        // Add Alexa notification services with default configuration
        services.AddAlexaNotifications();
    })
    .Build()
    .Run();


// ==============================================================================
// ADVANCED SETUP (With Custom Configuration)
// ==============================================================================
// If you need to customize how services are created:

services.AddAlexaNotifications(options => {
    // Customize how each component is created
    
    options.MessageFormatterFactory = (provider) => {
        // Custom MessageFormatter creation
        return new MessageFormatter();
    };
    
    options.VolumeManagerFactory = (ha, svcs, entities, devices) => {
        // Custom VolumeManager with additional logic
        var manager = new VolumeManager(ha, svcs, entities, devices);
        // ... additional initialization ...
        return manager;
    };
    
    options.PromptResponseSubjectFactory = () => {
        // Custom prompt response subject if needed
        return new System.Reactive.Subjects.Subject<PromptResponse>();
    };
});


// ==============================================================================
// USING THE SERVICE
// ==============================================================================
// Inject IAlexa into your app or controller:

public class MyAlexaApp
{
    private readonly IAlexa _alexa;

    public MyAlexaApp(IAlexa alexa)
    {
        _alexa = alexa;
    }

    public async Task SendAnnouncement()
    {
        // Use the injected Alexa service
        _alexa.Announce("media_player.living_room", "Hello, this is an announcement!");
    }
}


// ==============================================================================
// SERVICE REGISTRATION DETAILS
// ==============================================================================
// 
// The AddAlexaNotifications() extension registers:
//
// 1. Subject<PromptResponse> (Singleton)
//    - Shared event bus for prompt responses
//    - Single instance across the application
//
// 2. MessageFormatter (Singleton)
//    - Stateless, reusable formatter for messages
//    - Manages SSML formatting and message concatenation
//
// 3. VolumeManager (Scoped)
//    - Handles device volume management
//    - Depends on: IHaContext, IServices, IEntities, AlexaConfig
//
// 4. NotificationProcessor (Scoped)
//    - Orchestrates the notification processing pipeline
//    - Depends on: IEntities, IScheduler, VolumeManager, MessageFormatter, AlexaConfig
//
// 5. PromptResponseHandler (Scoped)
//    - Manages Home Assistant event subscriptions for prompt responses
//    - Depends on: Subject<PromptResponse>, AlexaConfig, ILogger
//
// 6. Alexa / IAlexa (Scoped)
//    - Main service that users interact with
//    - Implements IAlexa interface for public API
//    - Depends on: All of the above + IHaContext, IServices, IScheduler, IVoiceProvider
//


// ==============================================================================
// TESTING WITH DI
// ==============================================================================
// For unit tests, you can replace services:

[Test]
public void TestAlexaAnnouncement()
{
    var services = new ServiceCollection();
    
    // Add all Alexa services
    services.AddAlexaNotifications();
    
    // Or customize for testing:
    services.AddAlexaNotifications(options => {
        options.MessageFormatterFactory = sp => new MessageFormatter(); // Your test version
    });
    
    var provider = services.BuildServiceProvider();
    var alexa = provider.GetRequiredService<IAlexa>();
    
    // Now test with injected dependencies
    alexa.Announce("media_player.test", "Test message");
}


// ==============================================================================
// LIFETIME CONSIDERATIONS
// ==============================================================================
// 
// Singletons (created once, shared across application):
// - Subject<PromptResponse>  : Event bus, must be shared
// - MessageFormatter         : Stateless, safe to share
//
// Scoped (one instance per scope/request):
// - VolumeManager            : May hold state per request
// - NotificationProcessor    : Processes per request
// - PromptResponseHandler    : Manages subscriptions
// - Alexa / IAlexa           : Main service
//
// This design ensures:
// - Thread safety (scoped services not shared across requests)
// - Resource efficiency (singletons for stateless components)
// - Flexibility (easy to test or replace implementations)
//

