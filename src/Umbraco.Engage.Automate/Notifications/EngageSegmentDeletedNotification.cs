using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="SegmentDeletedEvent"/>.</summary>
public sealed class EngageSegmentDeletedNotification(SegmentDeletedEvent @event) : INotification
{
    public long SegmentId { get; } = @event.SegmentId;
}
