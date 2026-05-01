using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AbTestScheduledBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AbTestScheduledEvent>
{
    public void Handle(AbTestScheduledEvent @event) =>
        eventAggregator.Publish(new EngageAbTestScheduledNotification(@event));

    public override void Register() => SystemEventService.Register<AbTestScheduledEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
