using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="CustomerJourneyStepScoredEvent"/>.</summary>
public sealed class EngageCustomerJourneyStepScoredNotification(CustomerJourneyStepScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long CustomerJourneyStepId { get; } = @event.CustomerJourneyStepId;
    public int Score { get; } = @event.Score;
    public bool IsLocked { get; } = @event.IsLocked;
}
