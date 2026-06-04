## Umbraco.Engage.Automate

Umbraco Engage triggers and actions for Umbraco Automate - react to personalization events and run Engage operations from your automations.

### Features

- **20+ Triggers** - React to A/B testing, analytics, personalization, segment, customer journey, persona, goal, and campaign events (e.g. A/B Test Started, Segment Saved, Persona Scored, Custom Goal Completed)
- **3 Actions** - Trigger goals, score personas, and score customer journey steps for visitors from automation steps
- **Bridged Notifications** - Engage events are re-published as CMS notifications so Automate's trigger infrastructure can observe them
- **Zero Configuration** - Self-registers with Umbraco's composition pipeline; no further wiring required

Example: fire a goal when a form is submitted, or react when an A/B test goes live.

### Requirements

- Umbraco CMS 17.x
- Umbraco Engage 17.x
- Umbraco.Automate 17.0+
- .NET 10.0
