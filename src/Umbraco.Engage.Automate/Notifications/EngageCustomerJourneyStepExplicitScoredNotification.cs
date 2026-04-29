using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;
using Umbraco.Engage.Infrastructure.Personalization.CustomerJourney;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="CustomerJourneyStepExplicitScoredEvent"/>.</summary>
public sealed class EngageCustomerJourneyStepExplicitScoredNotification(CustomerJourneyStepExplicitScoredEvent @event) : INotification
{
    public Guid VisitorId { get; } = @event.VisitorId;
    public CustomerJourneyStepExplicitScore Entity { get; } = @event.Entity;
}
