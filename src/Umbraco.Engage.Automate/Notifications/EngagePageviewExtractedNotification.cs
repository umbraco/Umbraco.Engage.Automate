using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AnalyticsPageviewExtractedEvent"/>.</summary>
public sealed class EngagePageviewExtractedNotification(AnalyticsPageviewExtractedEvent @event) : INotification
{
    public long PageviewId { get; } = @event.Pageview.Id;
    public Guid PageviewGuid { get; } = @event.Pageview.Guid;
    public DateTime Timestamp { get; } = @event.Pageview.Timestamp;
    public long SessionId { get; } = @event.Pageview.Session.Id;
    public bool WasPersonalized { get; } = @event.Pageview.WasPersonalized;
}
