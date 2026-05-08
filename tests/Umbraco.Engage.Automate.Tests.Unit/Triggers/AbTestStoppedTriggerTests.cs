using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestStoppedTriggerTests
{
    private readonly AbTestStoppedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAbTestId()
    {
        const long abTestId = 99L;
        var notification = new EngageAbTestStoppedNotification(new AbTestStoppedEvent(abTestId));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
        ((TriggerEvent<AbTestStoppedTriggerOutput>)events[0]).Output.AbTestId.ShouldBe(abTestId);
    }
}
