using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.abTestStopped", "A/B Test Stopped",
    Description = "Fires when an A/B test is stopped.",
    Group = "Engage",
    Icon = "icon-stop")]
public sealed class AbTestStoppedTrigger
    : NotificationTriggerBase<object, AbTestStoppedTriggerOutput, EngageAbTestStoppedNotification>
{
    public AbTestStoppedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAbTestStoppedNotification notification)
    {
        yield return new TriggerEvent<AbTestStoppedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new AbTestStoppedTriggerOutput
            {
                AbTestId = notification.AbTestId,
            },
        };
    }
}
