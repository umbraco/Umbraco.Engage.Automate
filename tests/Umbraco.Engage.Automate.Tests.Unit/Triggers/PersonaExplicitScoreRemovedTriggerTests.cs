using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class PersonaExplicitScoreRemovedTriggerTests
{
    private readonly PersonaExplicitScoreRemovedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var visitorId = Guid.NewGuid();
        const long groupId = 4L;
        const long personaId = 8L;

        var engageEvent = new PersonaExplicitScoreRemovedEvent(visitorId, groupId, personaId);
        var notification = new EngagePersonaExplicitScoreRemovedNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<PersonaExplicitScoreRemovedTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.GroupId.ShouldBe(groupId);
        output.PersonaId.ShouldBe(personaId);
    }
}
