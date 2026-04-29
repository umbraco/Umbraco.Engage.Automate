using Umbraco.Cms.Core.Events;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate.Notifications.Handlers;

/// <summary>
/// Base class for Engage event bridge handlers.
/// Subclasses receive Engage system events and republish them as Umbraco CMS
/// notifications so Automate's trigger infrastructure can observe them.
/// </summary>
internal abstract class EngageBridgeHandlerBase : IEventHandler
{
    /// <summary>Registers this handler with Engage's SystemEventService.</summary>
    public abstract void Register();

    /// <summary>Unregisters this handler from Engage's SystemEventService.</summary>
    public abstract void Unregister();
}
