using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AbTestStoppedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AbTestStoppedEvent>
{
    public void Handle(AbTestStoppedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngageAbTestStoppedNotification(@event));

    public override void Register() => SystemEventService.Register<AbTestStoppedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
