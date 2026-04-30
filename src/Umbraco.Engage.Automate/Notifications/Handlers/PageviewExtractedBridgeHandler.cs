using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PageviewExtractedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AnalyticsPageviewExtractedEvent>
{
    public void Handle(AnalyticsPageviewExtractedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngagePageviewExtractedNotification(@event));

    public override void Register() => SystemEventService.Register<AnalyticsPageviewExtractedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
