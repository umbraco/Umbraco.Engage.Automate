using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PersonaExplicitScoreRemovedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<PersonaExplicitScoreRemovedEvent>
{
    public void Handle(PersonaExplicitScoreRemovedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngagePersonaExplicitScoreRemovedNotification(@event));

    public override void Register() => SystemEventService.Register<PersonaExplicitScoreRemovedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
