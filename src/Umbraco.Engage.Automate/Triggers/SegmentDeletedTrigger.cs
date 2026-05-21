using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.segmentDeleted", "Segment Deleted",
    Description = "Fires when a segment is deleted.",
    Group = "Engage",
    Icon = "icon-delete",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class SegmentDeletedTrigger
    : NotificationTriggerBase<object, SegmentDeletedTriggerOutput, EngageSegmentDeletedNotification>
{
    public SegmentDeletedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageSegmentDeletedNotification notification)
    {
        yield return new TriggerEvent<SegmentDeletedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new SegmentDeletedTriggerOutput
            {
                SegmentId = notification.SegmentId,
            },
        };
    }
}
