using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class CustomerJourneyStepExplicitScoreRemovedTriggerTests
{
    private readonly CustomerJourneyStepExplicitScoreRemovedTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var visitorId = Guid.NewGuid();
        const long groupId = 6L;
        const long stepId = 33L;

        var engageEvent = new CustomerJourneyStepExplicitScoreRemovedEvent(visitorId, groupId, stepId);
        var notification = new EngageCustomerJourneyStepExplicitScoreRemovedNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<CustomerJourneyStepExplicitScoreRemovedTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.GroupId.ShouldBe(groupId);
        output.CustomerJourneyStepId.ShouldBe(stepId);
    }
}
