using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.abTestVariantSaved", "A/B Test Variant Saved",
    Description = "Fires when an A/B test variant is saved.",
    Group = "Engage",
    Icon = "icon-science")]
public sealed class AbTestVariantSavedTrigger
    : NotificationTriggerBase<object, AbTestVariantSavedTriggerOutput, EngageAbTestVariantSavedNotification>
{
    public AbTestVariantSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAbTestVariantSavedNotification notification)
    {
        yield return new TriggerEvent<AbTestVariantSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new AbTestVariantSavedTriggerOutput
            {
                AbTestVariantId = notification.AbTestVariantId,
            },
        };
    }
}
