using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.Personas;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class PersonaExplicitScoredTriggerTests
{
    private readonly PersonaExplicitScoredTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var visitorId = Guid.NewGuid();
        var entity = new PersonaExplicitScore { PersonaId = 12, GroupId = 3 };

        var engageEvent = new PersonaExplicitScoredEvent(visitorId, entity);
        var notification = new EngagePersonaExplicitScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<PersonaExplicitScoredTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.PersonaId.ShouldBe(12L);
        output.GroupId.ShouldBe(3L);
    }
}
