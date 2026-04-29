using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestSavedEvent"/>.</summary>
public sealed class EngageAbTestSavedNotification(AbTestSavedEvent @event) : INotification
{
    public long AbTestId { get; } = @event.AbTestId;
}
