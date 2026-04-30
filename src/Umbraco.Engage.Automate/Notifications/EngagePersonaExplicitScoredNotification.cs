using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="PersonaExplicitScoredEvent"/>.</summary>
public sealed class EngagePersonaExplicitScoredNotification(PersonaExplicitScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long PersonaId { get; } = @event.Entity.PersonaId;
    public long GroupId { get; } = @event.Entity.GroupId;
}
