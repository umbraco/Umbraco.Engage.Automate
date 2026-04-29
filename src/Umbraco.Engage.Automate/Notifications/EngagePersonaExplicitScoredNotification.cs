using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.Personas;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="PersonaExplicitScoredEvent"/>.</summary>
public sealed class EngagePersonaExplicitScoredNotification(PersonaExplicitScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public PersonaExplicitScore Entity { get; } = @event.Entity;
}
