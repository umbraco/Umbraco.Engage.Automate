using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class SegmentDeletedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<SegmentDeletedEvent>
{
    public void Handle(SegmentDeletedEvent @event) =>
        eventAggregator.PublishAsync(new EngageSegmentDeletedNotification(@event)).GetAwaiter().GetResult();

    public override void Register() => SystemEventService.Register<SegmentDeletedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
