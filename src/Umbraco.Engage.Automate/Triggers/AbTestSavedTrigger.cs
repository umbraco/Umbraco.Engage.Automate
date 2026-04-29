using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.abTestSaved", "A/B Test Saved",
    Description = "Fires when an A/B test is saved.",
    Group = "Engage",
    Icon = "icon-science")]
public sealed class AbTestSavedTrigger
    : NotificationTriggerBase<object, AbTestSavedTriggerOutput, EngageAbTestSavedNotification>
{
    public AbTestSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAbTestSavedNotification notification)
    {
        yield return new TriggerEvent<AbTestSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            Output = new AbTestSavedTriggerOutput
            {
                AbTestId = notification.AbTestId,
            },
        };
    }
}
