using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class SegmentSavedTriggerTests
{
    [Fact]
    public void MapEvent_ReturnsCorrectSegmentId()
    {
        const long segmentId = 123L;
        var engageEvent = new SegmentSavedEvent(segmentId);
        var notification = new EngageSegmentSavedNotification(engageEvent);

        var events = TriggerTestHarness.For<SegmentSavedTrigger>()
            .MapEvent(notification)
            .ToList();

        events.ShouldHaveSingleItem();
        events[0].Output<SegmentSavedTriggerOutput>().SegmentId.ShouldBe(segmentId);
    }
}
