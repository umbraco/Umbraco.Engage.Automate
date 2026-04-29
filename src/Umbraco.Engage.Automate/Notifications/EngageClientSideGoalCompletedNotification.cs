using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Analytics.Goals;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="ClientSideGoalCompletedEvent"/>.</summary>
public sealed class EngageClientSideGoalCompletedNotification(ClientSideGoalCompletedEvent @event) : INotification
{
    public IPageview Pageview { get; } = @event.Pageview;
    public IGoalCompletion GoalCompletion { get; } = @event.GoalCompletion;
    public int SessionSequenceNumber { get; } = @event.SessionSequenceNumber;
}
