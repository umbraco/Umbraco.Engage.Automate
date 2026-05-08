using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AbTestStartedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AbTestStartedEvent>
{
    public void Handle(AbTestStartedEvent @event) =>
        eventAggregator.Publish(new EngageAbTestStartedNotification(@event));

    public override void Register() => SystemEventService.Register<AbTestStartedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
