using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.AbTesting.Models;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestStartedTriggerTests
{
    private readonly AbTestStartedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var abTest = new AbTest { Id = 99, Name = "My A/B Test" };
        var engageEvent = new AbTestStartedEvent(abTest);
        var notification = new EngageAbTestStartedNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<AbTestStartedTriggerOutput>)events[0]).Output;
        output.AbTestId.ShouldBe(99L);
        output.AbTestName.ShouldBe("My A/B Test");
    }
}
