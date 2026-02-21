using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Subjects;

namespace Niemand.Helpers.Notifications;

/// <summary>
/// Extension methods for registering Alexa notification services with the dependency injection container.
/// </summary>
public static class AlexaServiceCollectionExtensions
{
    /// <summary>
    /// Adds Alexa notification services to the service collection.
    /// Registers all required components for Alexa notifications to work.
    /// </summary>
    /// <remarks>
    /// This method should be called during application startup when configuring services.
    /// It registers the following services:
    /// - Subject&lt;PromptResponse&gt; (singleton - shared across all consumers)
    /// - MessageFormatter (singleton - stateless)
    /// - VolumeManager (scoped - dependent on IHaContext, IServices, IEntities)
    /// - NotificationProcessor (scoped - orchestrates the notification pipeline)
    /// - PromptResponseHandler (singleton - manages prompt response events, thread-safe subscription)
    /// - Alexa (scoped - the main orchestrator, implements IAlexa)
    /// </remarks>
    public static IServiceCollection AddAlexaNotifications(this IServiceCollection services)
    {
        // Register the prompt response subject as a singleton so it's shared
        services.AddSingleton(new Subject<PromptResponse>());

        // Register the message formatter as a singleton since it has no state
        services.AddSingleton<MessageFormatter>();

        // Register dependent services as scoped (one instance per request/scope)
        services.AddScoped(provider =>
        {
            var ha = provider.GetRequiredService<IHaContext>();
            var svcs = provider.GetRequiredService<IServices>();
            var entities = provider.GetRequiredService<IEntities>();
            var config = provider.GetRequiredService<IAppConfig<AlexaConfig>>();

            return new VolumeManager(ha, svcs, entities, config.Value.Devices);
        });

        services.AddScoped(provider =>
        {
            var entities = provider.GetRequiredService<IEntities>();
            var scheduler = provider.GetRequiredService<IScheduler>();
            var volumeManager = provider.GetRequiredService<VolumeManager>();
            var messageFormatter = provider.GetRequiredService<MessageFormatter>();
            var config = provider.GetRequiredService<IAppConfig<AlexaConfig>>();

            return new NotificationProcessor(entities, scheduler, volumeManager, messageFormatter, config.Value.Devices);
        });

        // Register PromptResponseHandler as a SINGLETON because it manages global event subscriptions
        // The SetupEventSubscription method uses a lock to ensure it only runs once
        // Note: Config is passed to SetupEventSubscription, not the constructor, to avoid scoped service dependency
        services.AddSingleton(provider =>
        {
            var promptResponses = provider.GetRequiredService<Subject<PromptResponse>>();
            var logger = provider.GetRequiredService<ILogger<PromptResponseHandler>>();

            return new PromptResponseHandler(promptResponses, logger);
        });

        // Register the main Alexa service as scoped
        services.AddScoped<IAlexa, Alexa>();

        return services;
    }

    /// <summary>
    /// Adds Alexa notification services with custom configuration.
    /// Useful for testing or advanced scenarios where you need to control the exact registration.
    /// </summary>
    public static IServiceCollection AddAlexaNotifications(
        this IServiceCollection services,
        Action<AlexaNotificationOptions> configureOptions)
    {
        var options = new AlexaNotificationOptions();
        configureOptions(options);

        // Register the prompt response subject
        services.AddSingleton(options.PromptResponseSubjectFactory?.Invoke() ?? new Subject<PromptResponse>());

        // Allow customization of service lifetimes
        services.AddScoped(sp => 
            options.MessageFormatterFactory?.Invoke(sp) ?? new MessageFormatter());

        services.AddScoped(provider =>
        {
            var ha = provider.GetRequiredService<IHaContext>();
            var svcs = provider.GetRequiredService<IServices>();
            var entities = provider.GetRequiredService<IEntities>();
            var config = provider.GetRequiredService<IAppConfig<AlexaConfig>>();

            return options.VolumeManagerFactory?.Invoke(ha, svcs, entities, config.Value.Devices) 
                ?? new VolumeManager(ha, svcs, entities, config.Value.Devices);
        });

        services.AddScoped(provider =>
        {
            var entities = provider.GetRequiredService<IEntities>();
            var scheduler = provider.GetRequiredService<IScheduler>();
            var volumeManager = provider.GetRequiredService<VolumeManager>();
            var messageFormatter = provider.GetRequiredService<MessageFormatter>();
            var config = provider.GetRequiredService<IAppConfig<AlexaConfig>>();

            return options.NotificationProcessorFactory?.Invoke(entities, scheduler, volumeManager, messageFormatter, config.Value.Devices)
                ?? new NotificationProcessor(entities, scheduler, volumeManager, messageFormatter, config.Value.Devices);
        });

        services.AddSingleton(provider =>
        {
            var promptResponses = provider.GetRequiredService<Subject<PromptResponse>>();
            var logger = provider.GetRequiredService<ILogger<PromptResponseHandler>>();

            return options.PromptResponseHandlerFactory?.Invoke(promptResponses, new(), logger)
                ?? new PromptResponseHandler(promptResponses, logger);
        });

        services.AddScoped<IAlexa, Alexa>();

        return services;
    }
}

/// <summary>
/// Configuration options for Alexa notification services.
/// Allows customization of how services are created and registered.
/// </summary>
public class AlexaNotificationOptions
{
    /// <summary>
    /// Factory for creating the shared PromptResponse subject.
    /// </summary>
    public Func<Subject<PromptResponse>>? PromptResponseSubjectFactory { get; set; }

    /// <summary>
    /// Factory for creating MessageFormatter instances.
    /// </summary>
    public Func<IServiceProvider, MessageFormatter>? MessageFormatterFactory { get; set; }

    /// <summary>
    /// Factory for creating VolumeManager instances.
    /// </summary>
    public Func<IHaContext, IServices, IEntities, IDictionary<string, AlexaDeviceConfig>, VolumeManager>? VolumeManagerFactory { get; set; }

    /// <summary>
    /// Factory for creating NotificationProcessor instances.
    /// </summary>
    public Func<IEntities, IScheduler, VolumeManager, MessageFormatter, IDictionary<string, AlexaDeviceConfig>, NotificationProcessor>? NotificationProcessorFactory { get; set; }

    /// <summary>
    /// Factory for creating PromptResponseHandler instances.
    /// </summary>
    public Func<Subject<PromptResponse>, Dictionary<string, AlexaPeopleConfig>, ILogger<PromptResponseHandler>, PromptResponseHandler>? PromptResponseHandlerFactory { get; set; }
}

