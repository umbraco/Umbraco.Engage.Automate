using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Automate.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

internal sealed class PersonaGroupSavedBridgeHandler(IEventAggregator eventAggregator)
    : EngageBridgeHandlerBase, IEventHandler<PersonaGroupSavedEvent>
{
    public void Handle(PersonaGroupSavedEvent @event) =>
        _ = eventAggregator.PublishAsync(new EngagePersonaGroupSavedNotification());

    public override void Register() => SystemEventService.Register<PersonaGroupSavedEvent>(this);
    public override void Unregister() => SystemEventService.Unregister(this);
}
