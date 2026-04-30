using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PersonaScoredBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<PersonaScoredEvent>
{
    public void Handle(PersonaScoredEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngagePersonaScoredNotification(@event));

    public override void Register() => SystemEventService.Register<PersonaScoredEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
