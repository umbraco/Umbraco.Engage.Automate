using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.appliedPersonalizationSaved", "Applied Personalization Saved",
    Description = "Fires when applied personalizations are saved.",
    Group = "Engage",
    Icon = "icon-settings")]
public sealed class AppliedPersonalizationSavedTrigger
    : NotificationTriggerBase<object, AppliedPersonalizationSavedTriggerOutput, EngageAppliedPersonalizationSavedNotification>
{
    public AppliedPersonalizationSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAppliedPersonalizationSavedNotification notification)
    {
        yield return new TriggerEvent<AppliedPersonalizationSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            Output = new AppliedPersonalizationSavedTriggerOutput(),
        };
    }
}
