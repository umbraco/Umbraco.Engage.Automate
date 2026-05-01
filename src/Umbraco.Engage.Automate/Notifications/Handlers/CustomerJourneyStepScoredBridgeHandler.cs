using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CustomerJourneyStepScoredBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CustomerJourneyStepScoredEvent>
{
    public void Handle(CustomerJourneyStepScoredEvent @event) =>
        eventAggregator.Publish(new EngageCustomerJourneyStepScoredNotification(@event));

    public override void Register() => SystemEventService.Register<CustomerJourneyStepScoredEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
