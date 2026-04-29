using Umbraco.Cms.Core.Notifications;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications;

/// <summary>CMS notification wrapper for Engage's <see cref="AbTestVariantSavedEvent"/>.</summary>
public sealed class EngageAbTestVariantSavedNotification(AbTestVariantSavedEvent @event) : INotification
{
    public long AbTestVariantId { get; } = @event.AbTestVariantId;
}
