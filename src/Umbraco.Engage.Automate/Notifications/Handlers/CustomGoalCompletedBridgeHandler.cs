using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class CustomGoalCompletedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<CustomGoalCompletedEvent>
{
    public void Handle(CustomGoalCompletedEvent @event) =>
        eventAggregator.Publish(new EngageCustomGoalCompletedNotification(@event));

    public override void Register() => SystemEventService.Register<CustomGoalCompletedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
