using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CampaignGroupSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CampaignGroupSavedEvent>
{
    public void Handle(CampaignGroupSavedEvent @event) =>
        eventAggregator.Publish(new EngageCampaignGroupSavedNotification());

    public override void Register() => SystemEventService.Register<CampaignGroupSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
