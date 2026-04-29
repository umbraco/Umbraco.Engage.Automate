using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="CustomGoalCompletedEvent"/>.</summary>
public sealed class EngageCustomGoalCompletedNotification(CustomGoalCompletedEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long GoalId { get; } = @event.GoalId;
    public int Value { get; } = @event.Value;
    public DateTime Timestamp { get; } = @event.Timestamp;
}
