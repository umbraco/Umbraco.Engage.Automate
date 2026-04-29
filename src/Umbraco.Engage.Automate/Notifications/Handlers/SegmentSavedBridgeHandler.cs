using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class SegmentSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<SegmentSavedEvent>
{
    public void Handle(SegmentSavedEvent @event) =>
        eventAggregator.PublishAsync(new EngageSegmentSavedNotification(@event)).GetAwaiter().GetResult();

    public override void Register() => SystemEventService.Register<SegmentSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
