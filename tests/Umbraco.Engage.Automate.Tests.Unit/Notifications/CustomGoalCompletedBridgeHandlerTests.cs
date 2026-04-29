using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Notifications;

public class CustomGoalCompletedBridgeHandlerTests
{
    [Fact]
    public void Handle_PublishesEngageCustomGoalCompletedNotification()
    {
        var eventAggregator = new Mock<IEventAggregator>();
        eventAggregator
            .Setup(ea => ea.PublishAsync(It.IsAny<EngageCustomGoalCompletedNotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var visitorId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var handler = new CustomGoalCompletedBridgeHandler(eventAggregator.Object);
        handler.Handle(new CustomGoalCompletedEvent(visitorId, goalId: 7, value: 100, timestamp));

        eventAggregator.Verify(
            ea => ea.PublishAsync(
                It.Is<EngageCustomGoalCompletedNotification>(n =>
                    n.VisitorId == visitorId &&
                    n.GoalId == 7 &&
                    n.Value == 100),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
