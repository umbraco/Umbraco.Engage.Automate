# Umbraco.Engage.Automate

Umbraco Engage triggers and actions for [Umbraco Automate](https://umbraco.com/products/umbraco-automate/).

## Overview

Umbraco.Engage.Automate is a provider package that connects [Umbraco Engage](https://umbraco.com/products/umbraco-engage/) to Umbraco Automate, exposing Engage's event pipeline and service layer as first-class triggers and actions in Automate flows — for example, firing a goal when a form is submitted, or reacting when an A/B test goes live.

## Key Features

- **20+ triggers** — react to A/B testing, analytics, personalization, segment, customer journey, persona, goal, and campaign events
- **3 actions** — score personas, score customer journey steps, and trigger goals from automation steps
- **Bridged notifications** — Engage events are re-published as CMS notifications so Automate's trigger infrastructure can observe them
- **Zero configuration** — `EngageAutomateComposer` self-registers with Umbraco's composition pipeline

## Installation

```bash
dotnet add package Umbraco.Engage.Automate
```

No further wiring is required — the composer is auto-discovered by Umbraco's composition system.

## Requirements

- .NET 10.0
- Umbraco CMS 17.x
- Umbraco Engage 17.x
- Umbraco.Automate 17.0+

## Triggers

Fire an Automate flow when something happens in Engage.

**A/B Testing**

| Trigger | Fires when… |
|---|---|
| A/B Test Saved | An A/B test is created or updated |
| A/B Test Scheduled | An A/B test is scheduled to start |
| A/B Test Started | An A/B test goes live |
| A/B Test Stopped | An A/B test is ended |
| A/B Test Variant Saved | A variant on an A/B test is saved |

**Analytics**

| Trigger | Fires when… |
|---|---|
| New Session Started | A new visitor session begins |
| Pageview Extracted | A pageview is processed by Engage |

**Personalization & Segments**

| Trigger | Fires when… |
|---|---|
| Applied Personalization Saved | A personalization rule is saved |
| Segment Saved | A segment is created or updated |
| Segment Deleted | A segment is deleted |

**Customer Journey**

| Trigger | Fires when… |
|---|---|
| Customer Journey Group Saved | A customer journey group is saved |
| Customer Journey Step Scored | A visitor scores a customer journey step |
| Customer Journey Step Explicitly Assigned | A visitor is explicitly assigned to a step |
| Customer Journey Step Assignment Removed | An explicit assignment is removed |

**Personas**

| Trigger | Fires when… |
|---|---|
| Persona Group Saved | A persona group is saved |
| Persona Scored | A visitor scores a persona |
| Persona Explicitly Assigned | A visitor is explicitly assigned to a persona |
| Persona Assignment Removed | An explicit persona assignment is removed |

**Goals & Campaigns**

| Trigger | Fires when… |
|---|---|
| Goals Saved | A goals configuration is saved |
| Custom Goal Completed | A custom goal is completed for a visitor |
| Client-Side Goal Completed | A client-side goal event fires |
| Campaign Group Saved | A campaign group is saved |

## Actions

Run Engage operations from an Automate flow.

| Action | What it does |
|---|---|
| Trigger Goal | Fires an Engage goal by key for a visitor |
| Score Persona | Adds a persona score for a visitor; optionally locks them to the persona |
| Score Customer Journey Step | Adds a customer journey step score for a visitor; optionally locks them to the step |

## How It Works

Engage publishes domain events through its own internal `SystemEventService` rather than Umbraco's standard `IEventAggregator`. Automate's trigger infrastructure observes `IEventAggregator`, so this package acts as a bridge:

```
Engage event → BridgeHandler → IEventAggregator notification → Automate trigger
```

On startup, `EngageAutomateComponent` registers lightweight bridge handlers with `SystemEventService`. Each handler converts the incoming Engage event into a typed CMS notification and re-publishes it via `IEventAggregator`. On shutdown, the component unregisters all handlers cleanly.

## Development

```bash
dotnet restore
dotnet build
dotnet test
```

### Project layout

```
src/
  Umbraco.Engage.Automate/           # Package source
    Actions/                         # Automate actions
    Notifications/                   # Bridge notification types
    Notifications/Handlers/          # Bridge handlers (Engage → CMS notifications)
    Triggers/                        # Automate triggers
tests/
  Umbraco.Engage.Automate.Tests.Unit/
```

## License

MIT — see [LICENSE](LICENSE) for details.
