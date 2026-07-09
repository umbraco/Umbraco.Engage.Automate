# CLAUDE.md

Guidance for Claude Code when working in this repository.

## Overview

Umbraco.Engage.Automate is a **provider package** that bridges [Umbraco Engage](https://umbraco.com/products/umbraco-engage/) (analytics/personalization) into [Umbraco Automate](https://umbraco.com/products/umbraco-automate/) (workflow automation for Umbraco CMS). It exposes Engage domain events as Automate triggers and Engage service calls as Automate actions. It is one of six `Umbraco.*.Automate` satellite add-on packages built the same way — see `.claude/skills/release-management/SKILL.md` and `.claude/skills/post-release-cleanup/SKILL.md` for this repo's release mechanics.

There is no `demo/` site in this repo (unlike some sibling satellites) — verification of trigger/action wiring is done via the unit test suite only, not a running Umbraco instance.

## 1. Architecture

- **Target framework:** `net10.0` (C# with nullable + implicit usings enabled, see `Directory.Build.props:9-11`)
- **Application type:** Class library (Umbraco CMS package/plugin), packed as a NuGet package
- **Depends on:** `Umbraco.Automate.Core` (trigger/action base classes) and `Umbraco.Engage.Core` (Engage domain services/events) — see `src/Umbraco.Engage.Automate/Umbraco.Engage.Automate.csproj:9-12`
- **Pattern:** Adapter/Bridge. Engage does not raise events through Umbraco's `IEventAggregator` — it has its own internal `SystemEventService` (`Umbraco.Engage.Infrastructure.Events`). This package's entire job is translating Engage's event model into Umbraco CMS notifications that Automate's trigger infrastructure can observe:

  ```
  Engage domain event → BridgeHandler.Handle() → eventAggregator.Publish(notification) → NotificationTriggerBase<>.MapEvent() → Automate flow run
  ```

### Solution structure

```
src/Umbraco.Engage.Automate/
  Actions/                    # 3 Automate actions (ActionBase<TSettings,TOutput> subclasses)
  Notifications/              # CMS notification DTOs wrapping Engage events (INotification)
  Notifications/Handlers/     # Bridge handlers: Engage SystemEventService → IEventAggregator
  Triggers/                   # 16 active Automate triggers (NotificationTriggerBase<> subclasses)
  Constants.cs                # Backoffice section alias used for RequiredSections gating
  EngageAutomateComponent.cs  # IAsyncComponent: registers/unregisters bridge handlers at startup/shutdown
  EngageAutomateComposer.cs   # IComposer: DI registration, auto-discovered by Umbraco composition
tests/Umbraco.Engage.Automate.Tests.Unit/
  Actions/, Triggers/, Notifications/
```

Every trigger/action/handler is a small, single-purpose class — there's no shared base beyond the Automate SDK types (`NotificationTriggerBase<TSettings, TOutput, TNotification>`, `ActionBase<TSettings, TOutput>`) and the internal `EngageBridgeHandlerBase` (`src/Umbraco.Engage.Automate/Notifications/Handlers/EngageBridgeHandlerBase.cs`). Adding a new trigger means adding four files (Notification, BridgeHandler, Trigger, TriggerOutput) plus two DI/lifecycle registrations — see Agentic Workflow below.

## 2. Commands

```bash
# Restore / build / test (from repo root)
dotnet restore Umbraco.Engage.Automate.slnx
dotnet build Umbraco.Engage.Automate.slnx --configuration Release
dotnet test Umbraco.Engage.Automate.slnx --configuration Release

# Pack (mirrors CI's build-and-pack.yml)
dotnet pack Umbraco.Engage.Automate.slnx --configuration Release --output ./artifacts

# Run a single test file/class
dotnet test tests/Umbraco.Engage.Automate.Tests.Unit --filter "FullyQualifiedName~AbTestSavedTrigger"
```

- **SDK:** pinned via `global.json` to `10.0.100` with `rollForward: latestFeature` — do not assume a different SDK is available.
- **Solution file:** `Umbraco.Engage.Automate.slnx` (the new XML-lite `.slnx` format, not `.sln`).
- **No demo/sample host** — there's nothing to `dotnet run`. This package only makes sense loaded inside a full Umbraco + Engage + Automate site, which this repo doesn't provide.
- **NuGet feeds** (`nuget.config`): nuget.org + Umbraco Nightly (myget) + Umbraco Prereleases (myget), with source mapping so `Umbraco*` packages resolve from the Umbraco feeds first. On `main`, `Directory.Packages.props` floats `Umbraco.Automate`/`.Core`/`.Testing` and `Umbraco.Engage.Core` to `[18.0.0, 18.999.999)`; on `support/17.x` these ranges are the v17 equivalent — see Teamwork section.

## 3. Style Guide

Nothing unusual beyond standard .NET conventions (`Nullable` + `ImplicitUsings` enabled repo-wide via `Directory.Build.props`). Two patterns worth knowing because every trigger/action follows them exactly:

- **Trigger declaration** (`src/Umbraco.Engage.Automate/Triggers/AbTestSavedTrigger.cs:6-14`): every trigger class carries a `[Trigger(alias, name, Description=, Group="Engage", Icon=, RequiredSections=[Constants.Sections.Engage])]` attribute and derives from `NotificationTriggerBase<object, TOutput, TNotification>`. The generic `object` first argument is the (unused) trigger-config type — none of the Engage triggers take configuration.
- **Action validation style** (`src/Umbraco.Engage.Automate/Actions/TriggerGoalAction.cs:24-31`, `ScorePersonaAction.cs:27-46`): actions parse string settings (e.g. GUID keys typed as `string` in the settings model for editor binding support) and return `ActionResult.Failed(exception, StepRunErrorCategory.Validation)` rather than throwing, for anything that's a bad input. Follow this — don't introduce thrown exceptions for user-input validation in a new action.
- Bridge handlers and the composer are `internal` — only `Constants`, triggers, actions, and their settings/output types are `public` (the package's actual surface area). `InternalsVisibleTo` grants the unit test project access (`Umbraco.Engage.Automate.csproj:14-16`).

## 4. Test Bench

- **Project:** `tests/Umbraco.Engage.Automate.Tests.Unit` — xUnit + Moq + Shouldly (`Using` aliases pre-imported globally via the csproj, so no `using Xunit;` etc. needed in test files).
- **Run:** `dotnet test Umbraco.Engage.Automate.slnx --configuration Release`
- **CI runs the matrix on Windows, Linux, and macOS** (`.devops/test.yml:8-17`) — don't rely on Windows-only path handling or line-ending assumptions in tests.
- **Coverage is asymmetric and worth knowing about:**
  - All 16 active triggers have a corresponding `*TriggerTests.cs` (added wholesale in commit `8d4e7cf`).
  - All 3 actions have tests.
  - Only **one** bridge handler has a test: `AbTestSavedBridgeHandlerTests.cs`. The other 15 bridge handlers (`Notifications/Handlers/*.cs`) are untested. The one test that exists is the reference pattern for testing a handler: mock `IEventAggregator`, call `Handle()` directly, verify `Publish` was called with the right notification shape (see `tests/Umbraco.Engage.Automate.Tests.Unit/Notifications/AbTestSavedBridgeHandlerTests.cs:10-22`).
  - If you touch a bridge handler, prefer adding its missing test rather than assuming the pattern is already covered.

## 5. Error Handling

- **Bridge handlers publish synchronously on purpose.** `eventAggregator.Publish(...)` (not an async publish) is used deliberately so that if a downstream Automate trigger handler throws, the exception propagates back through Engage's `SystemEventService` dispatch rather than being swallowed. This was explicitly fought for in history — commit `20cc7f2` ("Use synchronous Publish to surface handler exceptions") followed by `1c5d2e8` ("Merge main and revert bridge handlers to sync Publish") after a merge tried to move it back to async. **Do not "fix" this to `PublishAsync` or fire-and-forget** — that would silently hide trigger failures inside Engage's event pipeline.
- **Action validation failures** use `ActionResult.Failed(exception, StepRunErrorCategory.Validation)` for bad user input (invalid GUID strings, out-of-range values) — see `TriggerGoalAction.cs:27-31,37-39` and `ScorePersonaAction.cs:27-45`. This is an Automate SDK convention, not something introduced by this repo, but every action here follows it consistently — match it for new actions.

## 6. Clean Code

- **Bridge/Trigger separation is deliberate**, not incidental duplication: `Notifications/*.cs` are plain CMS `INotification` DTOs that intentionally decouple Automate triggers from Engage's internal event types (commit `7dcd9a5`, "Decouple notifications from internal Engage types"). Never have a `Trigger` class reference an Engage `Infrastructure.Events.*Event` type directly — always go through the notification wrapper, even though it means an extra class per trigger.
- **`EngageBridgeHandlerBase`** (`Notifications/Handlers/EngageBridgeHandlerBase.cs`) exists purely to give `Register()`/`Unregister()` a common shape for `EngageAutomateComponent` to call in a loop at startup/shutdown — it is not a place to put shared logic; each handler's `Handle()` is a one-line translation, keep it that way.
- **Section-gating via attribute, not code**: `RequiredSections = [Constants.Sections.Engage]` on every `[Trigger]`/`[Action]` attribute is how backoffice permission gating is expressed (added in commit `c320643`, "Gate Engage triggers and actions by section access"). If you add a new trigger/action and omit this, it will be selectable by users without Engage section access — always copy it from a sibling.

## 7. Security

- Permission model is entirely attribute-driven: `RequiredSections = [Constants.Sections.Engage]` (`Constants.cs:19`, alias `"Umb.Section.Engage"`). Automate's own infrastructure enforces this at the backoffice/editor level — this package does not re-check permissions in code, so don't add manual permission checks inside `ExecuteAsync`/`MapEvent`; the attribute is the single source of truth.
- No secrets, no auth tokens, no external HTTP calls in this package — it's purely in-process (Engage services and CMS notifications). Nothing to configure via User Secrets/Key Vault here.
- GUID-typed settings (`GoalKey`, `PersonaKey`, `VisitorExternalId`) are bound as `string` on the settings model to support Automate's flow-editor value bindings, then `Guid.TryParse`'d defensively in `ExecuteAsync` before use — never assume the string is a valid GUID by the time it reaches the action.

## 8. Teamwork and Workflow

- **Hosting:** GitHub, `github.com/umbraco/Umbraco.Engage.Automate` (`Directory.Build.props:17-18`). CI is Azure DevOps ("Umbraco Engage" project, definition 734).
- **Branch model:**
  - `main` = current CMS major line (**v18**)
  - `support/17.x` = previous CMS major line (**v17**) — checked out locally as a persistent worktree at `.claude/worktrees/support-17.x`
  - `release/YYYY.MM.N` branches are cut from whichever line is releasing; `N` is a **shared counter across both lines** in a given calendar month (e.g. `release/2026.07.1` for a v18 release, `release/2026.07.2` for a v17 release the same month) — never assume `N` restarts per line.
  - Active/historical feature branches seen in this repo's history: `feature/cms-v18-prep`, `feature/code-review`, `feature/disable-high-volume-triggers`, `feature/permission-gating`.
- **CI** (`azure-pipelines.yml` → `.devops/build-and-pack.yml` + `.devops/test.yml`): triggers on push to `main`, `dev`, `release/*`, `hotfix/*`, `feature/*`, and on PRs into `main`/`dev`. Two stages only: **Build & Pack** then **Test** (Windows/Linux/macOS matrix). SBOM generation via `cdxgen` is optional (`runSbom` parameter, default true) and uploads to Dependency-Track if `DT_API_KEY`/`DT_BASE_URL` are set. **There is no Publish stage** — nothing in CI pushes the built `.nupkg` anywhere.
- **Publishing is manual**: after CI is green on a `release/*` branch, a human downloads the `nupkg` pipeline artifact and pushes it to the MyGet feed by hand. Nothing automated does this.
- **Release process** is codified in this repo's own Claude Code skills — don't hand-roll it, invoke the skill instead:
  - `.claude/skills/release-management/SKILL.md` — cuts a `release/YYYY.MM.N` branch, bumps `Directory.Packages.props` Automate/Engage ranges to `[X.0.0, X.999.999)` for the target major, and sets `version.json` to the stable release version.
  - `.claude/skills/post-release-cleanup/SKILL.md` — run only after CI is green **and** a human has confirmed the manual MyGet push happened. Merges the release branch back (`--no-ff`) into its target, tags `release-<version>`, creates a GitHub Release via `gh release create <tag> --target <branch> --generate-notes`, patch-bumps `version.json` on the target branch for nightly builds, and deletes the release branch (local + remote).
- **Versioning:** Nerdbank.GitVersioning (`version.json`); `publicReleaseRefSpec` covers `main`, `hotfix/*`, `release/*` — builds off any other branch (including `support/17.x` directly) produce prerelease/non-public version strings, which is expected.
- **No `CONTRIBUTING.md` or PR template exists in this repo** — there's no documented PR checklist to follow beyond what CI enforces (build + test green). Commit message style in history is loosely Conventional-Commits-flavored (`fix(tests): ...`, `build(deps): ...`, `refactor(trigger): ...`, `chore(release): ...`) — follow that style for consistency even though it isn't enforced by tooling.

## 9. Edge Cases

- **High-volume triggers were deliberately disabled before initial launch and this is invisible from the README alone.** Commit `3f0dc20` ("disable high-volume visitor-event triggers for initial launch") removed the `Trigger`/`TriggerOutput`/`BridgeHandler` classes for six triggers that fire on nearly every visitor request rather than on an admin action:
  - New Session Started, Pageview Extracted
  - Customer Journey Step Scored (the *implicit* scoring event — not the `...ExplicitScored` one, which is still active)
  - Persona Scored (implicit — not `...ExplicitScored`, which is still active)
  - Custom Goal Completed, Client-Side Goal Completed

  Their `Notification` DTO classes (e.g. `Notifications/EngageNewSessionStartedNotification.cs`, `EngagePageviewExtractedNotification.cs`, `EngageCustomerJourneyStepScoredNotification.cs`, `EngagePersonaScoredNotification.cs`, `EngageCustomGoalCompletedNotification.cs`, `EngageClientSideGoalCompletedNotification.cs`) were **not** removed by the later cleanup commit `6c1dfba` ("Remove commented-out high-volume trigger code and orphaned files") — they still compile, are still `public`, but have no bridge handler publishing them and no trigger observing them. Treat them as intentionally-dormant scaffolding for a future opt-in/throttled reintroduction, not as dead code to casually delete, and not as a bug to "fix" by wiring them back up without a throttling story.
  - **`README.md`'s trigger tables (lines 35-90) still list all six as if they were shipping triggers.** This is stale documentation, not a second implementation you're missing — `EngageAutomateComponent.cs:29-49` (the `Register()`/`Unregister()` calls) is the actual source of truth for which of the 22 originally-planned triggers are live (16 are). If you're asked to add a trigger from the README's list and it's one of these six, flag that it's high-volume and was intentionally shelved before assuming it's a simple oversight.
- **Automate's trigger infrastructure only observes `IEventAggregator`**, so any new Engage event source that publishes through some other mechanism (not `SystemEventService`) needs its own bridge, not just a new `BridgeHandler` subclass following the existing pattern.

## 10. Agentic Workflow

- **Adding a new trigger** for an existing Engage event: create `Notifications/Engage<X>Notification.cs` (INotification DTO wrapping the Engage event), `Notifications/Handlers/<X>BridgeHandler.cs` (extends `EngageBridgeHandlerBase`, implements `IEventHandler<TEngageEvent>`), `Triggers/<X>Trigger.cs` + `<X>TriggerOutput.cs` (extends `NotificationTriggerBase<object, TOutput, TNotification>`), then wire the handler into `EngageAutomateComposer.Compose()` (DI registration) and `EngageAutomateComponent` (constructor param + `Register()`/`Unregister()` calls). Missing either registration means the trigger silently never fires — there's no runtime error, it just never gets called.
- **Before adding one of the six shelved high-volume triggers back**, re-read the Edge Cases entry above and confirm with the team whether the throttling/high-volume concern from `feature/disable-high-volume-triggers` has actually been addressed — don't just restore the deleted trigger classes.
- **Quality gate before considering work done:** `dotnet build` and `dotnet test` clean on the solution; if you touched a bridge handler, add the missing unit test rather than leaving the asymmetric coverage described in Test Bench.
- **Don't touch `RequiredSections` casually** — removing it from a trigger/action attribute changes who can see/use it in the backoffice; this is a permission decision, not a style choice.
- **When bumping Umbraco.Automate/Umbraco.Engage.Core versions**, that's a `Directory.Packages.props` edit — check whether it's a routine floor bump (patch/minor, safe) vs. a major bump (requires checking Automate/Engage SDK breaking changes first, per the "CMS v18 prep" pattern used across the Automate satellites).

## 11. Project-Specific Notes

- **This package has no logic of its own beyond translation.** Every trigger fires because Engage fired an event first; every action's business logic lives in Engage's own services (`IGoalService`, `IPersonaService`, `ICustomerJourneyService` via `Umbraco.Engage.Infrastructure.*`). This package is a thin adapter layer — if a bug report describes wrong *values* (not wrong *triggering behavior*), the bug is more likely in Engage itself than in this repo.
- **External integration surface:** `Umbraco.Engage.Core` (NuGet, floated to `[18.0.0, 18.999.999)` on `main`) and `Umbraco.Automate.Core`/`.Testing` (same floor pattern). Both are Umbraco-internal packages resolved from the Umbraco Nightly/Prereleases MyGet feeds per `nuget.config`'s source mapping — `nuget.org` is only the fallback/default source for everything that isn't `Umbraco*`.
- **Known limitation — 6 of 22 originally-scoped triggers are shipped-but-dormant** (see Edge Cases). This is the single most important thing to know before touching triggers in this repo: the README overstates what's active by design (it wasn't fully edited down after the launch-scope cut), and the component registration list in `EngageAutomateComponent.cs` is ground truth.
- **Bridge handler test coverage is 1/16** (see Test Bench) — this is real technical debt, not an oversight worth ignoring; if asked to improve test coverage in this repo, this is the highest-value place to start, since the one existing test (`AbTestSavedBridgeHandlerTests.cs`) is a ready-made template for the other 15.
- **Sync `Publish` in bridge handlers is a considered design decision, not an oversight** — see Error Handling. It was changed and reverted at least twice in history (`20cc7f2`, `1c5d2e8`), so if you're tempted to "improve" it to async, read that history first.
- **`InternalsVisibleTo Umbraco.Engage.Automate.Tests.Unit`** (csproj:14-16) is the only cross-assembly visibility exception; there's no separate integration test project or Testing helper package beyond `Umbraco.Automate.Testing` (used for base test infra, not integration hosting).
- **No `.editorconfig` exists either** (see Teamwork and Workflow for the `CONTRIBUTING.md`/PR-template gap) — style/process enforcement here is informal, not tooling-enforced beyond the nullable/implicit-usings compiler settings.

## Quick Reference

- **Build:** `dotnet build Umbraco.Engage.Automate.slnx --configuration Release`
- **Test:** `dotnet test Umbraco.Engage.Automate.slnx --configuration Release`
- **Pack:** `dotnet pack Umbraco.Engage.Automate.slnx --configuration Release --output ./artifacts`
- **Key projects:**
  - `src/Umbraco.Engage.Automate/Umbraco.Engage.Automate.csproj` — the package itself
  - `tests/Umbraco.Engage.Automate.Tests.Unit/Umbraco.Engage.Automate.Tests.Unit.csproj` — xUnit/Moq/Shouldly tests
- **Important files:**
  - `src/Umbraco.Engage.Automate/EngageAutomateComponent.cs` — ground truth for which triggers are actually live
  - `src/Umbraco.Engage.Automate/EngageAutomateComposer.cs` — DI wiring, auto-discovered by Umbraco composition (no manual `builder.AddEngageAutomate()` call needed by consumers)
  - `src/Umbraco.Engage.Automate/Constants.cs` — backoffice section gating constant
  - `version.json`, `Directory.Packages.props`, `nuget.config` — versioning/dependency floor configuration touched during releases
  - `.claude/skills/release-management/SKILL.md`, `.claude/skills/post-release-cleanup/SKILL.md` — this repo's release automation
- **Getting help:** README.md documents the intended (not always current — see Edge Cases) trigger/action catalogue; there's no separate architecture doc or wiki for this repo.
