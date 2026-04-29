using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class PersonaScoredTriggerTests
{
    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var visitorId = Guid.NewGuid();
        const long personaId = 7L;
        const int score = 50;
        const bool isLocked = true;

        var engageEvent = new PersonaScoredEvent(visitorId, personaId, score, isLocked);
        var notification = new EngagePersonaScoredNotification(engageEvent);

        var events = TriggerTestHarness.For<PersonaScoredTrigger>()
            .MapEvent(notification)
            .ToList();

        events.ShouldHaveSingleItem();

        var output = events[0].Output<PersonaScoredTriggerOutput>();
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

        var events = TriggerTestHarness.For<PersonaScoredTrigger>()
            .MapEvent(notification)
            .ToList();

        events[0].Output<PersonaScoredTriggerOutput>().IsLocked.ShouldBeFalse();
    }
}
