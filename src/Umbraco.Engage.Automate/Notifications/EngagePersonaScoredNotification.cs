using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="PersonaScoredEvent"/>.</summary>
public sealed class EngagePersonaScoredNotification(PersonaScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long PersonaId { get; } = @event.PersonaId;
    public int Score { get; } = @event.Score;
    public bool IsLocked { get; } = @event.IsLocked;
}
