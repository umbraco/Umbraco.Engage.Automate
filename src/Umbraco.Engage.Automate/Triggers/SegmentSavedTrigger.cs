using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.segmentSaved", "Segment Saved",
    Description = "Fires when a segment is saved.",
    Group = "Engage",
    Icon = "icon-users")]
public sealed class SegmentSavedTrigger
    : NotificationTriggerBase<object, SegmentSavedTriggerOutput, EngageSegmentSavedNotification>
{
    public SegmentSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageSegmentSavedNotification notification)
    {
        yield return new TriggerEvent<SegmentSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new SegmentSavedTriggerOutput
            {
                SegmentId = notification.SegmentId,
            },
        };
    }
}
