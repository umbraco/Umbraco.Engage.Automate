using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class SegmentDeletedTriggerTests
{
    private readonly SegmentDeletedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsSegmentId()
    {
        const long segmentId = 456L;
        var notification = new EngageSegmentDeletedNotification(new SegmentDeletedEvent(segmentId));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
        ((TriggerEvent<SegmentDeletedTriggerOutput>)events[0]).Output.SegmentId.ShouldBe(segmentId);
    }
}
