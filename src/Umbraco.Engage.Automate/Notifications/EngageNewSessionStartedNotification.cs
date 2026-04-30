using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AnalyticsNewSessionStartedEvent"/>.</summary>
public sealed class EngageNewSessionStartedNotification(AnalyticsNewSessionStartedEvent @event) : INotification
{
    public long SessionId { get; } = @event.Session.Id;
    public DateTime SessionTimestamp { get; } = @event.Session.Timestamp;
    public int PageviewCount { get; } = @event.Session.PageviewCount;
    public long? VisitorId { get; } = @event.Session.Visitor?.Id;
}
