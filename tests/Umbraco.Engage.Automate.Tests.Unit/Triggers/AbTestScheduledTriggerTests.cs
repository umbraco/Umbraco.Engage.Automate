using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.AbTesting.Models;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestScheduledTriggerTests
{
    private readonly AbTestScheduledTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAbTestIdAndName()
    {
        var abTest = new AbTest { Id = 42, Name = "Scheduled Test" };
        var notification = new EngageAbTestScheduledNotification(new AbTestScheduledEvent(abTest));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<AbTestScheduledTriggerOutput>)events[0]).Output;
        output.AbTestId.ShouldBe(42L);
        output.AbTestName.ShouldBe("Scheduled Test");
    }

    [Fact]
    public void MapEvent_NullName_AbTestNameIsNull()
    {
        var abTest = new AbTest { Id = 7, Name = null };
        var notification = new EngageAbTestScheduledNotification(new AbTestScheduledEvent(abTest));

        var events = _trigger.MapEvent(notification).ToList();

        ((TriggerEvent<AbTestScheduledTriggerOutput>)events[0]).Output.AbTestName.ShouldBeNull();
    }
}
