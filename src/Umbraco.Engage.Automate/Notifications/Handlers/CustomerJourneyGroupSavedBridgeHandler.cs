using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CustomerJourneyGroupSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CustomerJourneyGroupSavedEvent>
{
    public void Handle(CustomerJourneyGroupSavedEvent @event) =>
        eventAggregator.Publish(new EngageCustomerJourneyGroupSavedNotification());

    public override void Register() => SystemEventService.Register<CustomerJourneyGroupSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
