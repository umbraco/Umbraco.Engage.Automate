# Umbraco.Engage.Automate

Connects [Umbraco Engage](https://umbraco.com/products/umbraco-engage/) to [Umbraco Automate](https://umbraco.com/products/umbraco-automate/), exposing Engage's event pipeline and service layer as first-class triggers and actions in Automate flows.

> **Built heavily with [Claude](https://claude.ai) (Anthropic)** — this package was designed and implemented with AI-assisted development as an experiment in how far you can take AI pair programming on a real Umbraco package.

---

## What's in the box

### Triggers

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

**Personalization**
| Trigger | Fires when… |
|---|---|
| Applied Personalization Saved | A personalization rule is saved |

**Segments**
| Trigger | Fires when… |
|---|---|
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

**Goals**
| Trigger | Fires when… |
|---|---|
| Goals Saved | A goals configuration is saved |
| Custom Goal Completed | A custom goal is completed for a visitor |
| Client-Side Goal Completed | A client-side goal event fires |

**Campaigns**
| Trigger | Fires when… |
|---|---|
| Campaign Group Saved | A campaign group is saved |

---

### Actions

Run Engage operations from an Automate flow.

| Action | What it does |
|---|---|
| Trigger Goal | Fires an Engage goal by key for a visitor |
| Score Persona | Adds a persona score for a visitor; optionally locks them to the persona |
| Score Customer Journey Step | Adds a customer journey step score for a visitor; optionally locks them to the step |

---

## Installation

```bash
dotnet add package Umbraco.Engage.Automate
```

No further configuration is required. `EngageAutomateComposer` self-registers with Umbraco's composition pipeline and wires everything up automatically.

### Requirements

| Dependency | Version |
|---|---|
| .NET | 10 |
| Umbraco CMS | 17.x |
| Umbraco Engage | 17.x |
| Umbraco Automate | 0.1+ |

---

## How it works

Engage publishes domain events through its own internal `SystemEventService` rather than Umbraco's standard `IEventAggregator`. Automate's trigger infrastructure observes `IEventAggregator`, so this package acts as a bridge.

On startup, `EngageAutomateComponent` registers lightweight bridge handlers with `SystemEventService`. Each handler converts the incoming Engage event into a typed CMS notification and re-publishes it via `IEventAggregator`. Automate's trigger pipeline then picks it up in the normal way.

On shutdown, the component unregisters all handlers cleanly.

```
Engage event → BridgeHandler → IEventAggregator notification → Automate trigger
```

`EngageAutomateComposer` handles all DI registration — there is nothing for consumers to wire up manually.

---

## Development

### Building

```bash
dotnet restore
dotnet build
dotnet test
```

### Project layout

```
src/
  Umbraco.Engage.Automate/         # Package source
    Actions/                       # Automate actions
    Notifications/                 # Bridge notification types
    Notifications/Handlers/        # Bridge handlers (Engage → CMS notifications)
    Triggers/                      # Automate triggers
    EngageAutomateComposer.cs      # DI composition entry point
    EngageAutomateComponent.cs     # Startup/shutdown bridge wiring
tests/
  Umbraco.Engage.Automate.Tests.Unit/
    Actions/                       # Action tests
    Notifications/                 # Bridge handler tests
    Triggers/                      # Trigger tests
```

### CI pipeline

The Azure Pipelines workflow (`azure-pipelines.yml`) builds, runs tests cross-platform (Windows, Linux, macOS), and packages to a pipeline artifact. **It does not push to NuGet** — publishing is a manual, deliberate step.

---

## License

[MIT](LICENSE)

---

> *This package was built heavily with [Claude](https://claude.ai) by Anthropic as part of an experiment in AI-assisted Umbraco package development. The architecture, implementation, tests, and documentation were all produced through an iterative conversation with Claude Code.*
