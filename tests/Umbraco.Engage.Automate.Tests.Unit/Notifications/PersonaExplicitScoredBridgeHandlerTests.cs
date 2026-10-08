using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.Personas;

namespace Umbraco.Engage.Automate.Tests.Unit.Notifications;

public class PersonaExplicitScoredBridgeHandlerTests
{
    [Fact]
    public void Handle_PublishesEngagePersonaExplicitScoredNotification()
    {
        var eventAggregator = new Mock<IEventAggregator>();
        var visitorId = Guid.NewGuid();

        var handler = new PersonaExplicitScoredBridgeHandler(eventAggregator.Object);
        handler.Handle(new PersonaExplicitScoredEvent(visitorId, new PersonaExplicitScore { PersonaId = 42, GroupId = 7 }));

        eventAggregator.Verify(
            ea => ea.Publish(
                It.Is<EngagePersonaExplicitScoredNotification>(n =>
                    n.VisitorId == visitorId && n.PersonaId == 42 && n.GroupId == 7)),
            Times.Once);
    }
}
