using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AnalyticsPageviewExtractedEvent"/>.</summary>
public sealed class EngagePageviewExtractedNotification(AnalyticsPageviewExtractedEvent @event) : INotification
{
    public IPageview Pageview { get; } = @event.Pageview;
}
