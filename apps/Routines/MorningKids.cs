

using HomeAssistantGenerated;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Niemand;

[NetDaemonApp]
[Focus]
public class MorningKids
{
    private readonly IEntities _entities;
    private readonly IServices _services;
    private readonly IScheduler _scheduler;
    private readonly ILogger<MorningKids> _logger;
    private readonly RgbwFrameBuilder fb;

    public MorningKids(IEntities entities, IServices services, IScheduler scheduler, ILogger<MorningKids> logger)
    {
        _entities = entities;
        _services = services;
        _scheduler = scheduler;
        _logger = logger;
        fb = new RgbwFrameBuilder(numLeds: 30);
        _scheduler.SchedulePeriodic(TimeSpan.FromMinutes(1), () => Update());
    }

    void Update()
    {
        var now = _scheduler.Now.LocalDateTime;
        var current_time = TimeOnly.FromDateTime(now);
        if (current_time < TimeOnly.Parse("06:45") || current_time > TimeOnly.Parse("08:00"))
        {
            _entities.Light.PicoWLedsPicoWLeds.TurnOff();
            return;
        }

        if (_entities.Light.PicoWLedsPicoWLeds.IsOff())
            _entities.Light.PicoWLedsPicoWLeds.TurnOn(new LightTurnOnParameters
            {
                Effect = "MQTT Frame"
            });

        var total = TimeSpan.FromMinutes(60);
        var perLed = TimeSpan.FromSeconds((int)Math.Ceiling(total.TotalSeconds / 30));
        var endsAt = now + total;

        var remaining = endsAt - now;
        if (remaining < TimeSpan.Zero) remaining = TimeSpan.Zero;

        // How many LEDs lit for remaining time (countdown style)
        int lit = (int)Math.Ceiling(remaining.TotalSeconds / perLed.TotalSeconds);
        lit = Math.Abs(30 - Math.Clamp(lit, 0, fb.NumLeds));

        // Choose a style (pick ONE):
        byte[] baseFrame = fb.BuildSegmentedBar(lit);   // traffic-light (recommended)
                                                        // byte[] baseFrame = fb.BuildGradientBar(lit); // continuous gradient alternative
        _logger.LogDebug("Time Remaining: {remaining}, LEDs Lit: {lit} ", remaining, lit);
        // Send base frame

        string hex = RgbwFrameBuilder.ToHex(baseFrame);
        _services.Esphome.PicoWLedsPushFrameHex(hex);
        

        // Optional: attention wipe (fast burst to draw eye)
        // Send 30–40 fps over ~300–500 ms
        //foreach (var f in fb.AttentionWipe(baseFrame, width: 2, boostPct: 40, forward: true))
        //{
        //    SendFrame(f);
        //    System.Threading.Thread.Sleep(33); // ~30 fps (use async delay in NetDaemon)
        //}
    }
}



public sealed class RgbwFrameBuilder
{
    public int NumLeds { get; }
    private static readonly byte[] P = Enumerable.Range(0, 101)
        .Select(p => (byte)Math.Clamp(Math.Round(p / 100.0 * 255.0), 0, 255))
        .ToArray();

    public RgbwFrameBuilder(int numLeds) => NumLeds = numLeds;

    public byte[] Empty() => new byte[NumLeds * 4];

    public static string ToHex(ReadOnlySpan<byte> data)
    {
        var c = new char[data.Length * 2];
        const string hex = "0123456789ABCDEF";
        for (int i = 0; i < data.Length; i++)
        {
            byte b = data[i];
            c[2 * i] = hex[b >> 4]; c[2 * i + 1] = hex[b & 0xF];
        }
        return new string(c);
    }

    // --- Color helpers (0..100% inputs) ---
    private static void PutRgbw(byte[] frame, int i, int r, int g, int b, int w)
    {
        int off = i * 4;
        frame[off + 0] = P[Math.Clamp(r, 0, 100)];
        frame[off + 1] = P[Math.Clamp(g, 0, 100)];
        frame[off + 2] = P[Math.Clamp(b, 0, 100)];
        frame[off + 3] = P[Math.Clamp(w, 0, 100)];
    }

    private static (int R, int G, int B) Hsv(double h, double s, double v)
    {
        // h in [0,360), s,v in [0,1]
        double c = v * s, x = c * (1 - Math.Abs((h / 60) % 2 - 1)), m = v - c;
        double r = 0, g = 0, b = 0;
        int hi = (int)(h / 60) % 6;
        switch (hi)
        {
            case 0: r = c; g = x; b = 0; break;
            case 1: r = x; g = c; b = 0; break;
            case 2: r = 0; g = c; b = x; break;
            case 3: r = 0; g = x; b = c; break;
            case 4: r = x; g = 0; b = c; break;
            case 5: r = c; g = 0; b = x; break;
        }
        return ((int)Math.Round((r + m) * 100),
                (int)Math.Round((g + m) * 100),
                (int)Math.Round((b + m) * 100));
    }

    // --- Progress models ---

    /// <summary>
    /// Segmented traffic-light style: 3 blocks of 10 LEDs (Green/Amber/Red).
    /// Each block ramps 10%..100% brightness across its 10 LEDs (more obvious).
    /// </summary>
    public byte[] BuildSegmentedBar(int litCount, int greenPct = 100, int amberPct = 100, int redPct = 100)
    {
        litCount = Math.Clamp(litCount, 0, NumLeds);
        var f = Empty();
        for (int i = 0; i < litCount; i++)
        {
            int posInBlock = i % 10;                // 0..9
            int ramp = (posInBlock + 1) * 10;         // 10..100
            int r = 0, g = 0, b = 0, w = 0;

            if (i < 10) { g = (int)(ramp * (greenPct / 100.0)); }
            else if (i < 20) { r = (int)(ramp * 1.0); g = (int)(ramp * 0.6 * (amberPct / 100.0)); } // amber = R strong + some G
            else { r = (int)(ramp * (redPct / 100.0)); }

            PutRgbw(f, i, r, g, b, w);
        }
        return f;
    }

    /// <summary>
    /// Continuous gradient bar from Green (start) through Amber to Red (end).
    /// Easier code, but sometimes less obvious at a glance.
    /// </summary>
    public byte[] BuildGradientBar(int litCount)
    {
        litCount = Math.Clamp(litCount, 0, NumLeds);
        var f = Empty();
        for (int i = 0; i < litCount; i++)
        {
            double t = (NumLeds == 1) ? 0 : i / (double)(NumLeds - 1);
            // Hue 120° (green) -> 60° (amber) -> 0° (red)
            double h = 120 * (1.0 - t);
            var (r, g, b) = Hsv(h, 1.0, 1.0); // full saturation/value
            PutRgbw(f, i, r, g, b, 0);
        }
        return f;
    }

    /// <summary>
    /// Overlay a short wipe/scan highlight onto an existing frame to draw attention.
    /// Returns a sequence of frames you can send quickly (e.g., every 30–40 ms).
    /// </summary>
    public IEnumerable<byte[]> AttentionWipe(byte[] baseFrame, int width = 2, int boostPct = 40, bool forward = true)
    {
        width = Math.Clamp(width, 1, Math.Max(1, NumLeds / 4));
        for (int head = 0; head < NumLeds; head++)
        {
            int h = forward ? head : (NumLeds - 1 - head);
            var f = (byte[])baseFrame.Clone();
            for (int k = 0; k < width; k++)
            {
                int idx = h - k;
                if (idx < 0) break;
                int off = idx * 4;
                // Boost each channel by boostPct (clamped)
                for (int c = 0; c < 4; c++)
                {
                    int v = f[off + c];
                    int boosted = (int)Math.Round(v * (1 + boostPct / 100.0));
                    f[off + c] = (byte)Math.Clamp(boosted, 0, 255);
                }
            }
            yield return f;
        }
    }
}
