using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestStartedEvent"/>.</summary>
public sealed class EngageAbTestStartedNotification(AbTestStartedEvent @event) : INotification
{
    public long AbTestId { get; } = @event.Test.Id;
    public string? AbTestName { get; } = @event.Test.Name;
}
