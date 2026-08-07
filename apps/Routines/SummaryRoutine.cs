using HomeAssistantGenerated;
using Niemand.Helpers;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace Niemand;

[NetDaemonApp]
//[Focus]
public class SummaryRoutine : IAsyncInitializable
{
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly IAlexa _alexa;
    private readonly ILogger<SummaryRoutine> _logger;

    public SummaryRoutine(IEntities entities, IServices services, IAlexa alexa, IScheduler scheduler, ILogger<SummaryRoutine> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _alexa = alexa;
        _logger = logger;
    }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        _entities.InputButton.MorningSummary.StateChanges().Subscribe(state =>
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await Run("https://n8n.niemand.uk/webhook/morning_summary").ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing scheduled run for morning");
                }
            });
        });

        _entities.InputButton.EveningSummary.StateChanges().Subscribe(state =>
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await Run("https://n8n.niemand.uk/webhook/evening_summary").ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing scheduled run for evening");
                }
            });
        });

        // Morning at 07:00
        _scheduler.ScheduleCron("0 7 * * MON-FRI", () =>
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await Run("https://n8n.niemand.uk/webhook/morning_summary").ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing scheduled run for morning");
                }
            });
        });

        // Evening at 19:00
        _scheduler.ScheduleCron("0 19 * * MON-FRI", () =>
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await Run("https://n8n.niemand.uk/webhook/evening_summary").ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing scheduled run for evening");
                }
            });
        });

        return Task.CompletedTask;
    }

    private async Task Run(string url)
    {
        var summary = await FetchAndHandleAsync(url);
        if (!string.IsNullOrWhiteSpace(summary))
        {
            var msg = summary + ",,Todays Joke,," + _entities.Sensor.Joke.Attributes?.Joke;
            _alexa.Announce(new Alexa.Config() { Entity = "media_player.everywhere_2", Message = msg, VolumeLevel = 0.4, Whisper = false });
            _services.Notify.Twinstead(new NotifyTwinsteadParameters() { Title = "Summary", Message = msg });
        }
        else
        {
            _logger.LogWarning("No summary to announce for {Url}", url);
        }
    }

    private async Task<string> FetchAndHandleAsync(string url)
    {
        try
        {
            _logger.LogTrace("Fetching payload from {Url}...", url);

            var payload = await FetchPayloadAsync(url).ConfigureAwait(false);

            // Log the full payload (beware of very large bodies)
            _logger.LogTrace("Fetched payload from {Url}: {Payload}", url, payload);

            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(payload);
            if (dict != null && dict.TryGetValue("summary", out var summaryFromDict))
            {
                _logger.LogTrace("Parsed summary (dict): {Summary}", summaryFromDict);
                return summaryFromDict;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching payload from {Url}", url);
            return string.Empty;
        }
    }

    private static async Task<string> FetchPayloadAsync(string url)
    {
        using var http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(180)
        };

        using var response = await http.GetAsync(url).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }


}