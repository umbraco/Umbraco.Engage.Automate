using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="CustomerJourneyStepExplicitScoreRemovedEvent"/>.</summary>
public sealed class EngageCustomerJourneyStepExplicitScoreRemovedNotification(CustomerJourneyStepExplicitScoreRemovedEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public long GroupId { get; } = @event.GroupId;
    public long CustomerJourneyStepId { get; } = @event.CustomerJourneyStepId;
}
