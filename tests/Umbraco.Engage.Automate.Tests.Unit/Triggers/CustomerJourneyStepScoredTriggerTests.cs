using Umbraco.Automate.Core.Settings;
using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Automate.Triggers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Tests.Unit.Triggers;

public class CustomerJourneyStepScoredTriggerTests
{
    private readonly CustomerJourneyStepScoredTrigger _trigger = new(
        new TriggerInfrastructure(Mock.Of<IEditableModelResolver>()));

    [Fact]
    public void MapEvent_MapsAllProperties()
    {
        var visitorId = Guid.NewGuid();
        const long stepId = 15L;
        const int score = 75;

        var engageEvent = new CustomerJourneyStepScoredEvent(visitorId, stepId, score, isLocked: true);
        var notification = new EngageCustomerJourneyStepScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        events.ShouldHaveSingleItem();

        var output = ((TriggerEvent<CustomerJourneyStepScoredTriggerOutput>)events[0]).Output;
        output.VisitorId.ShouldBe(visitorId);
        output.CustomerJourneyStepId.ShouldBe(stepId);
        output.Score.ShouldBe(score);
        output.IsLocked.ShouldBeTrue();
    }

    [Fact]
    public void MapEvent_NotLocked_IsLockedFalse()
    {
        var engageEvent = new CustomerJourneyStepScoredEvent(Guid.NewGuid(), 1L, 10, isLocked: false);
        var notification = new EngageCustomerJourneyStepScoredNotification(engageEvent);

        var events = _trigger.MapEvent(notification).ToList();

        ((TriggerEvent<CustomerJourneyStepScoredTriggerOutput>)events[0]).Output.IsLocked.ShouldBeFalse();
    }
}
