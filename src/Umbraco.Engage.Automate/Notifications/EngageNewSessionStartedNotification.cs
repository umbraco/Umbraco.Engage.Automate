using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AnalyticsNewSessionStartedEvent"/>.</summary>
public sealed class EngageNewSessionStartedNotification(AnalyticsNewSessionStartedEvent @event) : INotification
{
    public ISession Session { get; } = @event.Session;
}
