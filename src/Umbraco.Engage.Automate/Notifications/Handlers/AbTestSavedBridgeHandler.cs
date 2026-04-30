using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AbTestSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AbTestSavedEvent>
{
    public void Handle(AbTestSavedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngageAbTestSavedNotification(@event));

    public override void Register() => SystemEventService.Register<AbTestSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
