using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class PersonaScoredTriggerTests
{
    private readonly PersonaScoredTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var visitorId = Guid.NewGuid();
        const long personaId = 7L;
        const int score = 50;
        const bool isLocked = true;

        var engageEvent = new PersonaScoredEvent(visitorId, personaId, score, isLocked);
        var notification = new EngagePersonaScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<PersonaScoredTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.PersonaId.ShouldBe(personaId);
        output.Score.ShouldBe(score);
        output.IsLocked.ShouldBeTrue();
    }

    [Fact]
    public void MapEvent_NotLocked_IsLockedFalse()
    {
        var engageEvent = new PersonaScoredEvent(Guid.NewGuid(), 1L, 10, false);
        var notification = new EngagePersonaScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        ((TriggerEvent<PersonaScoredTriggerOutput>)events[0]).Output.IsLocked.ShouldBeFalse();
    }
}
