using HomeAssistantGenerated;
using Microsoft.Extensions.DependencyInjection;

namespace Niemand.Helpers;

public static class Extentions
{
    public static IServiceCollection AddGeneratedCode(this IServiceCollection serviceCollection)
        => serviceCollection
           .AddTransient<Entities>()
           .AddTransient<Services>();
}