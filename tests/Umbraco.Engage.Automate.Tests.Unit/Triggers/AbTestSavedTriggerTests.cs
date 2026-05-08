using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class AbTestSavedTriggerTests
{
    private readonly AbTestSavedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAbTestId()
    {
        const long abTestId = 42L;
        var notification = new EngageAbTestSavedNotification(new AbTestSavedEvent(abTestId));

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
        ((TriggerEvent<AbTestSavedTriggerOutput>)events[0]).Output.AbTestId.ShouldBe(abTestId);
    }
}
