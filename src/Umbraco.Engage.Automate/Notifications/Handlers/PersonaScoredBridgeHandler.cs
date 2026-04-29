using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PersonaScoredBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<PersonaScoredEvent>
{
    public void Handle(PersonaScoredEvent @event) =>
        eventAggregator.PublishAsync(new EngagePersonaScoredNotification(@event)).GetAwaiter().GetResult();

    public override void Register() => SystemEventService.Register<PersonaScoredEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
