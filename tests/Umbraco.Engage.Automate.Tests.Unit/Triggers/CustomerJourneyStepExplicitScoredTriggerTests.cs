using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.CustomerJourney;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class CustomerJourneyStepExplicitScoredTriggerTests
{
    private readonly CustomerJourneyStepExplicitScoredTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var visitorId = Guid.NewGuid();
        var entity = new CustomerJourneyStepExplicitScore { CustomerJourneyStepId = 25, GroupId = 2 };

        var engageEvent = new CustomerJourneyStepExplicitScoredEvent(visitorId, entity);
        var notification = new EngageCustomerJourneyStepExplicitScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<CustomerJourneyStepExplicitScoredTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.CustomerJourneyStepId.ShouldBe(25L);
        output.GroupId.ShouldBe(2L);
    }
}
