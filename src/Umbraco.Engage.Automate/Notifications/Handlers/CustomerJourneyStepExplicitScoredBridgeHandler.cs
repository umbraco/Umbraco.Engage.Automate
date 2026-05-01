using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CustomerJourneyStepExplicitScoredBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CustomerJourneyStepExplicitScoredEvent>
{
    public void Handle(CustomerJourneyStepExplicitScoredEvent @event) =>
        eventAggregator.Publish(new EngageCustomerJourneyStepExplicitScoredNotification(@event));

    public override void Register() => SystemEventService.Register<CustomerJourneyStepExplicitScoredEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
