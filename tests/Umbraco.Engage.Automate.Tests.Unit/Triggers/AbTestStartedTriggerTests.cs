using Umbraco.Automate.Testing;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.AbTesting.Models;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestStartedTriggerTests
{
    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var abTest = new AbTest { Id = 99, Name = "My A/B Test" };
        var engageEvent = new AbTestStartedEvent(abTest);
        var notification = new EngageAbTestStartedNotification(engageEvent);

        var events = TriggerTestHarness.For<AbTestStartedTrigger>()
            .MapEvent(notification)
            .ToList();

        events.ShouldHaveSingleItem();

        var output = events[0].Output<AbTestStartedTriggerOutput>();
        output.AbTestId.ShouldBe(99L);
        output.AbTestName.ShouldBe("My A/B Test");
    }
}
