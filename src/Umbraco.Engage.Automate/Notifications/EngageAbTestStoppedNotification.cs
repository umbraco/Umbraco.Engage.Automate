using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestStoppedEvent"/>.</summary>
public sealed class EngageAbTestStoppedNotification(AbTestStoppedEvent @event) : INotification
{
    public long AbTestId { get; } = @event.AbTestId;
}
