using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class NewSessionStartedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AnalyticsNewSessionStartedEvent>
{
    public void Handle(AnalyticsNewSessionStartedEvent @event) =>
        eventAggregator.PublishAsync(new EngageNewSessionStartedNotification(@event)).GetAwaiter().GetResult();

    public override void Register() => SystemEventService.Register<AnalyticsNewSessionStartedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
