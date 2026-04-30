using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class GoalsSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<GoalsSavedEvent>
{
    public void Handle(GoalsSavedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngageGoalsSavedNotification());

    public override void Register() => SystemEventService.Register<GoalsSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
