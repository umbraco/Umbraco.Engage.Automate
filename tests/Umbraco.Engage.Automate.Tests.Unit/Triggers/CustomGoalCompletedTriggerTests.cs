using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class CustomGoalCompletedTriggerTests
{
    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var visitorId = Guid.NewGuid();
        const long goalId = 42L;
        const int value = 10;
        var timestamp = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        var engageEvent = new CustomGoalCompletedEvent(visitorId, goalId, value, timestamp);
        var notification = new EngageCustomGoalCompletedNotification(engageEvent);

        var events = TriggerTestHarness.For<CustomGoalCompletedTrigger>()
            .MapEvent(notification)
            .ToList();

        events.ShouldHaveSingleItem();

        var output = events[0].Output<CustomGoalCompletedTriggerOutput>();
        output.VisitorId.ShouldBe(visitorId);
        output.GoalId.ShouldBe(goalId);
        output.Value.ShouldBe(value);
        output.Timestamp.ShouldBe(timestamp);
    }
}
