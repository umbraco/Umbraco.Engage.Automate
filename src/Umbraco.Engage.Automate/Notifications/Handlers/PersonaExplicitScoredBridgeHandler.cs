using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PersonaExplicitScoredBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<PersonaExplicitScoredEvent>
{
    public void Handle(PersonaExplicitScoredEvent @event) =>
        eventAggregator.Publish(new EngagePersonaExplicitScoredNotification(@event));

    public override void Register() => SystemEventService.Register<PersonaExplicitScoredEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
