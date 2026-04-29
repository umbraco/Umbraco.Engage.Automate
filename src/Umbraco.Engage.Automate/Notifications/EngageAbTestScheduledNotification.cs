using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.AbTesting.Models;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestScheduledEvent"/>.</summary>
public sealed class EngageAbTestScheduledNotification(AbTestScheduledEvent @event) : INotification
{
    public AbTest Test { get; } = @event.Test;
}
