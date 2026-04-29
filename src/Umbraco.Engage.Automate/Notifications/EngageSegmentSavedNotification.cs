using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="SegmentSavedEvent"/>.</summary>
public sealed class EngageSegmentSavedNotification(SegmentSavedEvent @event) : INotification
{
    public long SegmentId { get; } = @event.SegmentId;
}
