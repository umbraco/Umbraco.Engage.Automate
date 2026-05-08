using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="CustomerJourneyStepExplicitScoredEvent"/>.</summary>
public sealed class EngageCustomerJourneyStepExplicitScoredNotification(CustomerJourneyStepExplicitScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long CustomerJourneyStepId { get; } = @event.Entity.CustomerJourneyStepId;
    public long GroupId { get; } = @event.Entity.GroupId;
}
