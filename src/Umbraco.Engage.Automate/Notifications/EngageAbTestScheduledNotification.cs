using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestScheduledEvent"/>.</summary>
public sealed class EngageAbTestScheduledNotification(AbTestScheduledEvent @event) : INotification
{
    public long AbTestId { get; } = @event.Test.Id;
    public string? AbTestName { get; } = @event.Test.Name;
}
