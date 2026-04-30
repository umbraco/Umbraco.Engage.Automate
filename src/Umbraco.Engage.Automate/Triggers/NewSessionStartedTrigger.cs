using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.newSessionStarted", "New Session Started",
    Description = "Fires when a new visitor session starts.",
    Group = "Engage",
    Icon = "icon-user")]
public sealed class NewSessionStartedTrigger
    : NotificationTriggerBase<object, NewSessionStartedTriggerOutput, EngageNewSessionStartedNotification>
{
    public NewSessionStartedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageNewSessionStartedNotification notification)
    {
        yield return new TriggerEvent<NewSessionStartedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new NewSessionStartedTriggerOutput
            {
                SessionId = notification.SessionId,
                SessionTimestamp = notification.SessionTimestamp,
                PageviewCount = notification.PageviewCount,
                VisitorId = notification.VisitorId,
            },
        };
    }
}
