# Umbraco Engage Automate

Umbraco Engage triggers and actions for [Umbraco Automate](https://umbraco.com), exposing the Umbraco Engage event pipeline and service layer as building blocks for Automate flows.

## What's in the box

### Triggers

Fire an Automate flow when something happens in Engage.

**A/B Testing**
- A/B Test Saved
- A/B Test Scheduled
- A/B Test Started
- A/B Test Stopped
- A/B Test Variant Saved

**Analytics**
- New Session Started
- Pageview Extracted

**Personalization**
- Applied Personalization Saved

**Segments**
- Segment Saved
- Segment Deleted

**Customer Journey**
- Customer Journey Group Saved
- Customer Journey Step Scored
- Customer Journey Step Explicitly Assigned
- Customer Journey Step Assignment Removed

**Personas**
- Persona Group Saved
- Persona Scored
- Persona Explicitly Assigned
- Persona Assignment Removed

**Goals**
- Goals Saved
- Custom Goal Completed
- Client-Side Goal Completed

**Campaigns**
- Campaign Group Saved

### Actions

Run Engage operations from an Automate flow.

**Goals**
- Trigger Goal — fires an Engage goal by its key

**Personas**
- Score Persona — adds a persona score for a visitor; optionally locks the visitor to the persona

**Customer Journey**
- Score Customer Journey Step — adds a customer journey step score for a visitor; optionally locks the visitor to the step

## Installation

```bash
dotnet add package Umbraco.Engage.Automate
```

The `EngageAutomateComposer` auto-registers with Umbraco's composition pipeline — no further wiring is required. It bridges Engage's event notifications into CMS notifications so Automate's trigger infrastructure can observe them.

### Requirements

- Umbraco CMS 17
- Umbraco Engage 17
- Umbraco Automate 0.1+
- .NET 10

## How it works

Engage uses its own `SystemEventService` to publish domain events. This package bridges those events into Umbraco's CMS notification pipeline (`IEventAggregator`), which Automate's trigger infrastructure observes.

The `EngageAutomateComponent` registers bridge handlers with `SystemEventService` during application startup and unregisters them on shutdown. Each bridge handler converts an Engage event into a CMS notification and publishes it, allowing Automate triggers to react.

The `EngageAutomateComposer` handles all registration automatically — no manual wiring is required by users of this package.

## Demo

There is currently no dedicated Engage demo site comparable to the Commerce demo store. To test the package locally, add it to an existing Umbraco 17 site that has Umbraco Engage installed.

## Repository layout

- `src/Umbraco.Engage.Automate` — the package
- `tests/Umbraco.Engage.Automate.Tests.Unit` — unit tests for triggers, actions, and bridge handlers

## License

[MIT](LICENSE)
