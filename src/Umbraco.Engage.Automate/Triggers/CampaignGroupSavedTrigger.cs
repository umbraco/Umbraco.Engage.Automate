using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.campaignGroupSaved", "Campaign Group Saved",
    Description = "Fires when a campaign group is saved.",
    Group = "Engage",
    Icon = "icon-megaphone")]
public sealed class CampaignGroupSavedTrigger
    : NotificationTriggerBase<object, CampaignGroupSavedTriggerOutput, EngageCampaignGroupSavedNotification>
{
    public CampaignGroupSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCampaignGroupSavedNotification notification)
    {
        yield return new TriggerEvent<CampaignGroupSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            Output = new CampaignGroupSavedTriggerOutput(),
        };
    }
}
