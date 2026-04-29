using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CustomerJourneyStepExplicitScoreRemovedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CustomerJourneyStepExplicitScoreRemovedEvent>
{
    public void Handle(CustomerJourneyStepExplicitScoreRemovedEvent @event) =>
        eventAggregator.PublishAsync(new EngageCustomerJourneyStepExplicitScoreRemovedNotification(@event)).GetAwaiter().GetResult();

    public override void Register() => SystemEventService.Register<CustomerJourneyStepExplicitScoreRemovedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
