using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class SegmentSavedTriggerTests
{
    private readonly SegmentSavedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_ReturnsCorrectSegmentId()
    {
        const long segmentId = 123L;
        var engageEvent = new SegmentSavedEvent(segmentId);
        var notification = new EngageSegmentSavedNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
        ((TriggerEvent<SegmentSavedTriggerOutput>)events[0]).Output.SegmentId.ShouldBe(segmentId);
    }
}
