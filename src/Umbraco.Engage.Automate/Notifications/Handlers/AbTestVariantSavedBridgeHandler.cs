using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AbTestVariantSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AbTestVariantSavedEvent>
{
    public void Handle(AbTestVariantSavedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngageAbTestVariantSavedNotification(@event));

    public override void Register() => SystemEventService.Register<AbTestVariantSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
