using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.CustomerJourney;

namespace Umbraco.Engage.Automate.Tests.Unit.Notifications;

public class CustomerJourneyStepExplicitScoredBridgeHandlerTests
{
    [Fact]
    public void Handle_PublishesEngageCustomerJourneyStepExplicitScoredNotification()
    {
        var eventAggregator = new Mock<IEventAggregator>();
        var visitorId = Guid.NewGuid();

        var handler = new CustomerJourneyStepExplicitScoredBridgeHandler(eventAggregator.Object);
        handler.Handle(new CustomerJourneyStepExplicitScoredEvent(
            visitorId, new CustomerJourneyStepExplicitScore { CustomerJourneyStepId = 42, GroupId = 7 }));

        eventAggregator.Verify(
            ea => ea.Publish(
                It.Is<EngageCustomerJourneyStepExplicitScoredNotification>(n =>
                    n.VisitorId == visitorId && n.CustomerJourneyStepId == 42 && n.GroupId == 7)),
            Times.Once);
    }
}
