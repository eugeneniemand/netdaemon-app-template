## Quick context for code-generation and edits

This repo is a NetDaemon app-template (C#/.NET) that hosts multiple Home Assistant automation apps compiled into a single `daemonapp` executable.

- Entrypoint: `program.cs` — builds a Generic Host using NetDaemon runtime and registers apps from the current assembly.
- Project file: `daemonapp.csproj` — target framework `net9.0`. App YAML files under `apps/**/*.yaml` are copied into output. Several subfolders under `apps/` contain each app as its own folder (e.g. `apps/Kitchen/Kitchen.cs`).
- Auto-generated Hass model: `HomeAssistantGenerated.cs` — large generated file mapping Home Assistant entities to strongly-typed classes. Keep this in-sync with the codegen tool.
- Configuration: `appsettings.json` — Home Assistant connection and local logging configuration. Secrets/tokens often live here or via environment vars (see README).

## Architecture & key patterns

- Single-process host — all apps are registered and run inside one .NET Generic Host (see `program.cs`). Apps are simple classes annotated with `[NetDaemonApp]` and rely on DI to receive `IHaContext`, `IScheduler`, `IEntities`, `IServices`, `ILogger<T>`, etc.
- Codegen: The repository uses NetDaemon HassModel code generator to create `HomeAssistantGenerated.cs`. Generated types are registered via `.AddHomeAssistantGenerated()` in `program.cs` and injected into apps as `IEntities` and `IServices`.
- Apps live in `apps/<AppName>/` and typically follow this pattern:
  - A configuration POCO (example: `KitchenConfiguration`) registered via `IAppConfig<T>`.
  - Subscribe to entity observables using `.StateChanges()` and `ToBooleanObservable()`.
  - Use `IScheduler.Sleep(...)` for async waits inside app logic.
  - Use `ILogger<T>` for structured logs. Logs are configured via Serilog in `appsettings.json`.

## Build / test / run workflows (practical commands)

- Restore and build (local development):
  dotnet restore
  dotnet build ./daemonapp.csproj -c Debug

- Run locally (dynamic compile disabled):
  dotnet run --project ./daemonapp.csproj

- Run unit tests (primary test project):
  dotnet test ./Niemand.Tests/Niemand.Tests.csproj

  Note: there is also a `tests/daemonapp_test.csproj` present in the workspace. Use `Niemand.Tests` as the canonical test suite for this repository unless you have a specific reason to run the separate `tests/` project.

- Publish for deployment (container/addon or publish folder):
  dotnet publish -c Release ./daemonapp.csproj

Notes:
- There are VS Code tasks in the workspace for `build`, `test`, and `publish`. The built-in `test` task in this workspace runs the older `tests/daemonapp_test.csproj` and may target a different framework; prefer calling the `Niemand.Tests` command above when developing locally.
- `appsettings.json` is copied to output (see csproj) — update tokens either there or via environment variables: `HOMEASSISTANT__HOST`, `HOMEASSISTANT__TOKEN`, `NETDAEMON__APPSOURCE`, etc.

### Known test helper & test failures

- The `Niemand.Tests` project references a local test helper project outside the repo at `..\..\netdaemon-extentions-testing\NetDaemon.Extensions.Testing` (see `Niemand.Tests.csproj` ProjectReference). If that external project fails to build, you'll see errors about missing `IServices` members (for example `IServices.Vacuum` or `IServices.Wiser`). Fixes:
  - Update the external `NetDaemon.Extensions.Testing` project to implement any newly-added `IServices` members, or
  - Align NetDaemon package versions between `daemonapp` and the testing helper.

- Recent local test run (developer environment) produced:
  - Build succeeded for `daemonapp` and the external test helper.
  - `Niemand.Tests` ran 48 tests: 46 succeeded, 2 failed.
    - `CoffeeMachinePower_TurnsOnLight` failed with a NullReferenceException originating from `apps/Kitchen/Kitchen.cs` during a NumericSensor state-change handler.
    - `CheapEnergyActive_TriggersNotification` failed an assertion expecting a `notify.twinstead` service call.

  When you see these failures:
  - Re-run `dotnet test ./Niemand.Tests/Niemand.Tests.csproj` to reproduce and view stack traces.
  - Inspect the failing test stack trace to find the null value (example: check configured `KitchenConfiguration` or fixtures creating mock entities).
  - If the failures are in the external test helper behaviour (state-change simulation or service-call recording), update the helper or the test accordingly.


## Conventions & gotchas for editing

- Keep `HomeAssistantGenerated.cs` in sync with installed NetDaemon nugets. Re-generate with the nd-codegen tool documented at top of the file:
  dotnet tool run nd-codegen

- App classes should be registered by the runtime via `AddAppsFromAssembly(Assembly.GetExecutingAssembly())`. New app classes must be public and annotated `[NetDaemonApp]`.

- Use strongly-typed generated entities from `IEntities` (e.g. `entities.BinarySensor.KitchenMotion`) rather than string entity ids when possible.

- Avoid committing secrets. `appsettings.json` in this repo is used for development; prefer env vars in CI or deployment.

## Tests & mocking patterns

- Unit tests live under `Niemand.Tests/` and use a local HaContext mock (`tC:\eugene\netdaemon-extentions-testing\NetDaemon.Extensions.Testing\NetDaemon.Extensions.Testing.csproj`). `IHaContext` is mocked and injected through DI.

- When adding unit tests, prefer exercising app logic via the same observables used in production (StateChanges, ToBooleanObservable) to keep test semantics close to runtime behavior.

## Useful files to reference when implementing or altering behavior

- `program.cs` — host & service registration
- `daemonapp.csproj` — copy/publish rules and package references
- `HomeAssistantGenerated.cs` — entity/service mappings and codegen instructions
- `appsettings.json` — runtime configuration (Serilog, HomeAssistant connection, NetDaemon settings)
- `apps/` — each app is a subfolder with `.cs` and optional `.yaml` files (see `apps/Kitchen/Kitchen.cs` for an idiomatic example)
- `CustomLoggingProvider.cs` — custom logging extension used by `program.cs`

## When making PRs or code changes — checklist for the agent

1. Preserve the generated file boundaries: do not manually edit `HomeAssistantGenerated.cs`; instead re-run the codegen and commit the updated generated file.
2. Build locally (`dotnet build`) and run unit tests (`dotnet test`) before pushing.
3. If adding new YAML files under `apps/`, ensure the csproj copies them to output (csproj already includes a wildcard for `apps\**\*.yaml`).
4. Keep logging levels and Serilog sinks consistent with `appsettings.json` naming.

If anything in this file is unclear or you want more examples from specific apps, tell me which app or file and I will extend this guide.
