using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Notifications;

public class AbTestSavedBridgeHandlerTests
{
    [Fact]
    public void Handle_PublishesEngageAbTestSavedNotification()
    {
        var eventAggregator = new Mock<IEventAggregator>();

        var handler = new AbTestSavedBridgeHandler(eventAggregator.Object);
        handler.Handle(new AbTestSavedEvent(abTestId: 42));

        eventAggregator.Verify(
            ea => ea.Publish(
                It.Is<EngageAbTestSavedNotification>(n => n.AbTestId == 42)),
            Times.Once);
    }
}
