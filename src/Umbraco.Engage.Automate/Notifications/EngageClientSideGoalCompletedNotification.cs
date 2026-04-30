using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="ClientSideGoalCompletedEvent"/>.</summary>
public sealed class EngageClientSideGoalCompletedNotification(ClientSideGoalCompletedEvent @event) : INotification
{
    public long PageviewId { get; } = @event.Pageview.Id;
    public Guid PageviewGuid { get; } = @event.Pageview.Guid;
    public long GoalId { get; } = @event.GoalCompletion.Goal.Id;
    public decimal GoalValue { get; } = @event.GoalCompletion.Value;
    public DateTime Timestamp { get; } = @event.GoalCompletion.Timestamp;
    public int SessionSequenceNumber { get; } = @event.SessionSequenceNumber;
}
