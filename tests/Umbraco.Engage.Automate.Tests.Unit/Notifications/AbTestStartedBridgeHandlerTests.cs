using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.AbTesting.Models;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Notifications;

public class AbTestStartedBridgeHandlerTests
{
    [Fact]
    public void Handle_PublishesEngageAbTestStartedNotification()
    {
        var eventAggregator = new Mock<IEventAggregator>();

        var handler = new AbTestStartedBridgeHandler(eventAggregator.Object);
        handler.Handle(new AbTestStartedEvent(new AbTest { Id = 42, Name = "Hero banner" }));

        eventAggregator.Verify(
            ea => ea.Publish(
                It.Is<EngageAbTestStartedNotification>(n => n.AbTestId == 42 && n.AbTestName == "Hero banner")),
            Times.Once);
    }
}
