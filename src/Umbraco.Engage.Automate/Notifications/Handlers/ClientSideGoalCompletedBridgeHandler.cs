using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class ClientSideGoalCompletedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<ClientSideGoalCompletedEvent>
{
    public void Handle(ClientSideGoalCompletedEvent @event) =>
        eventAggregator.Publish(new EngageClientSideGoalCompletedNotification(@event));

    public override void Register() => SystemEventService.Register<ClientSideGoalCompletedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
