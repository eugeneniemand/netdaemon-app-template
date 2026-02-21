# NetDaemon Home Automation Apps

A comprehensive C# / .NET 10 home automation application built with [NetDaemon](https://netdaemon.xyz) for Home Assistant, featuring intelligent lighting management, energy monitoring, notifications, routines, and more.

## Overview

This repository contains a collection of production-ready home automation applications that integrate with Home Assistant via NetDaemon. It provides intelligent automation for lights, energy management, security, notifications, appliance monitoring, and household routines.

### Key Features

- **Intelligent Lighting Management** - Motion-triggered lights with brightness/color adaptation, night mode support, and occupancy awareness
- **Energy Cost Optimization** - Real-time energy rate monitoring with cost-aware scheduling and notifications
- **Smart Notifications** - Multi-channel notifications (Telegram, Alexa, push) for appliances, batteries, and events
- **Household Routines** - Morning routines, evening summaries, train schedules, and custom automations
- **Security & Monitoring** - Motion alerts, door monitoring, security system integration, and watchdog functions
- **Appliance Management** - Automatic notifications for appliance cycles (dishwasher, dryer, washer)
- **Kids Features** - Screen time management, chores tracking, and kid-friendly routines

## Quick Start

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Home Assistant](https://www.home-assistant.io/) instance
- Home Assistant Long-Lived Access Token
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/eugeneniemand/netdaemon-app-template.git
   cd netdaemon-app-template
   ```

2. **Configure Home Assistant connection**
   
   Edit `appsettings.json`:
   ```json
   {
     "HomeAssistant": {
       "Host": "your-ha-instance.local",
       "Port": 8123,
       "Ssl": false,
       "Token": "your-long-lived-access-token"
     },
     "Mqtt": {
       "Host": "your-mqtt-broker.local"
     }
   }
   ```

3. **Restore dependencies and build**
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

> **Security Note**: Never commit sensitive information like tokens or IP addresses. Use environment variables for production deployment.

## Project Structure

```
.
├── apps/
│   ├── LightsManager/          # Motion-triggered lighting with color/brightness control
│   ├── Energy/                 # Energy rate monitoring and cost optimization
│   ├── NotificationsManager/   # Multi-channel notification system
│   ├── Security/               # Security monitoring and door watchdog
│   ├── Routines/               # Morning, evening, and custom routines
│   ├── Monzo/                  # Financial transaction monitoring
│   ├── Kids/                   # Screen time and chores management
│   └── [Other apps]/           # Additional automation apps
├── Helpers/                    # Shared utilities and extensions
├── daemonapp.csproj           # Main project file
├── program.cs                 # Application entry point
├── appsettings.json           # Configuration
└── Niemand.Tests/             # Unit tests
```

## Apps Overview

### LightsManager
Sophisticated motion-triggered lighting system with:
- Per-room configuration via YAML
- Automatic brightness and color temperature adjustment
- Night mode support
- Lux-based activation thresholds
- Manual override with automatic reset
- Circadian rhythm integration

### Energy App
Monitors Octopus Energy rates and:
- Identifies cheapest energy windows (1h, 2h, 3h)
- Sends notifications and Alexa announcements
- Schedules automations during cheap periods
- Supports appliance automation

### NotificationsManager
Handles notifications across multiple channels:
- Telegram messaging
- Alexa announcements and prompts
- Push notifications
- Appliance cycle notifications (dishwasher, dryer, washer)
- Battery status monitoring

### Security
Provides:
- Motion detection alerts
- Door monitoring and logging
- Security system integration
- Watchdog functionality

### Routines
Scheduled automations including:
- Morning routines for families
- Evening summaries and notifications
- Train schedule announcements
- Discipline management for kids
- Travel planning assistance

## Configuration

### LightsManager Configuration

Configure rooms in `apps/LightsManager/LightsManager.yaml`:

```yaml
LightManagerV2.ManagerConfig:
  NdUserId: your-user-id
  GuardTimeout: 300
  Rooms:
    - Name: Study
      Timeout: 300                          # Auto-off timeout (seconds)
      NightTimeout: 90                      # Night mode timeout
      PresenceEntities:
        - binary_sensor.office_motion
      ControlEntities:
        - light.office
      NightControlEntities:
        - light.office
      LuxEntity: sensor.office_lux
      LuxlimitEntity: input_number.office_lux_limit
      NightTimeEntity: input_select.house_mode
      NightTimeEntityStates:
        - night
        - sleeping
```

### Logging Configuration

Control log levels in `appsettings.json`:
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "System": "Information",
        "Microsoft": "Information"
      }
    }
  }
}
```

## Deployment

### Local Development

```bash
dotnet run
```

### Docker

Build and run using Docker:
```bash
docker build -t netdaemon-apps .
docker run -v ~/netdaemon:/data netdaemon-apps
```

### Home Assistant Add-on

Follow the [NetDaemon installation guide](https://netdaemon.xyz/docs/started/installation) to deploy as a Home Assistant add-on.

### Publish for Production

```bash
dotnet publish -c Release -o ./publish
```

Copy the contents of the `publish` folder to your deployment target.

## Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `HOMEASSISTANT__HOST` | Home Assistant hostname/IP | Required |
| `HOMEASSISTANT__PORT` | Home Assistant port | 8123 |
| `HOMEASSISTANT__TOKEN` | Long-lived access token | Required |
| `HOMEASSISTANT__SSL` | Use HTTPS | false |
| `MQTT__HOST` | MQTT broker hostname/IP | Optional |
| `NETDAEMON__GENERATEENTITIES` | Auto-generate entity stubs | false |

## Development

### Running Tests

```bash
dotnet test
```

### Code Structure

- **Apps** inherit from `NetDaemonApp` attribute
- **Services** injected via dependency injection
- **Reactive subscriptions** for entity state changes
- **YAML configuration** for per-app settings

### Example App Structure

```csharp
[NetDaemonApp]
public class MyApp
{
    private readonly IHaContext _haContext;
    private readonly IScheduler _scheduler;
    private readonly ILogger<MyApp> _logger;

    public MyApp(IHaContext haContext, IScheduler scheduler, ILogger<MyApp> logger)
    {
        _haContext = haContext;
        _scheduler = scheduler;
        _logger = logger;

        // Subscribe to state changes
        _haContext.Entity("sensor.temperature")
            .StateChanges()
            .Subscribe(change => Handle(change));
    }
}
```

## Technologies Used

- **NetDaemon** - Home Assistant automation framework
- **.NET 10** - Runtime and framework
- **C# 12** - Language
- **Reactive Extensions (Rx.NET)** - Event handling
- **YAML** - Configuration
- **Serilog** - Logging
- **Polly** - Resilience patterns
- **Stateless** - State machine management

## Support & Resources

- [NetDaemon Documentation](https://netdaemon.xyz)
- [Home Assistant Docs](https://www.home-assistant.io/docs/)
- [NetDaemon Discord Community](https://discord.gg/K3xwfcX)

## Known Limitations

- Requires persistent Home Assistant instance
- MQTT integration is optional but recommended for reliability
- Some features depend on specific Home Assistant integrations (Octopus Energy, etc.)

## Related Repositories

- [NetDaemon](https://github.com/net-daemon/netdaemon) - Main framework
- [NetDaemon Extensions Testing](https://github.com/eugeneniemand/NetDaemon.Extensions.Testing) - Testing utilities
- [NetDaemon App Template](https://github.com/net-daemon/netdaemon-app-template) - Official template

---

**Last Updated**: January 2025  
**Target Framework**: .NET 10.0  
**Language Version**: C# 12

