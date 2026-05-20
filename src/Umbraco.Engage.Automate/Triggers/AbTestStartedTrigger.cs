using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.abTestStarted", "A/B Test Started",
    Description = "Fires when an A/B test starts running.",
    Group = "Engage",
    Icon = "icon-play",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class AbTestStartedTrigger
    : NotificationTriggerBase<object, AbTestStartedTriggerOutput, EngageAbTestStartedNotification>
{
    public AbTestStartedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAbTestStartedNotification notification)
    {
        yield return new TriggerEvent<AbTestStartedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new AbTestStartedTriggerOutput
            {
                AbTestId = notification.AbTestId,
                AbTestName = notification.AbTestName,
            },
        };
    }
}
