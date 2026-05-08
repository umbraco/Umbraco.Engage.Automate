using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Analytics.Goals;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class ClientSideGoalCompletedTriggerTests
{
    private readonly ClientSideGoalCompletedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var pageviewGuid = Guid.NewGuid();
        var goalTimestamp = new DateTime(2026, 1, 15, 12, 30, 0, DateTimeKind.Utc);
        const long pageviewId = 100L;
        const long goalId = 50L;
        const decimal goalValue = 25.50m;
        const int sessionSequenceNumber = 3;

        var goal = new Mock<IGoal>();
        goal.Setup(g => g.Id).Returns(goalId);

        var goalCompletion = new Mock<IGoalCompletion>();
        goalCompletion.Setup(gc => gc.Goal).Returns(goal.Object);
        goalCompletion.Setup(gc => gc.Value).Returns(goalValue);
        goalCompletion.Setup(gc => gc.Timestamp).Returns(goalTimestamp);

        var pageview = new Mock<IPageview>();
        pageview.Setup(p => p.Id).Returns(pageviewId);
        pageview.Setup(p => p.Guid).Returns(pageviewGuid);

        var engageEvent = new ClientSideGoalCompletedEvent(pageview.Object, goalCompletion.Object, sessionSequenceNumber);
        var notification = new EngageClientSideGoalCompletedNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<ClientSideGoalCompletedTriggerOutput>)events[0]).Output;
        output.PageviewId.ShouldBe(pageviewId);
        output.PageviewGuid.ShouldBe(pageviewGuid);
        output.GoalId.ShouldBe(goalId);
        output.GoalValue.ShouldBe(goalValue);
        output.Timestamp.ShouldBe(goalTimestamp);
        output.SessionSequenceNumber.ShouldBe(sessionSequenceNumber);
    }
}
