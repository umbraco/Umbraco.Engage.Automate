using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class AppliedPersonalizationSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<AppliedPersonalizationSavedEvent>
{
    public void Handle(AppliedPersonalizationSavedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngageAppliedPersonalizationSavedNotification());

    public override void Register() => SystemEventService.Register<AppliedPersonalizationSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
