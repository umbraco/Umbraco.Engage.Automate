using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class CustomerJourneyGroupSavedTriggerTests
{
    private readonly CustomerJourneyGroupSavedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_ReturnsCorrectTriggerEvent()
    {
        var notification = new EngageCustomerJourneyGroupSavedNotification();

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();
    }
}
