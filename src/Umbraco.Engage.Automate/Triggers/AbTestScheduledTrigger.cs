using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.abTestScheduled", "A/B Test Scheduled",
    Description = "Fires when an A/B test is scheduled to start.",
    Group = "Engage",
    Icon = "icon-calendar")]
public sealed class AbTestScheduledTrigger
    : NotificationTriggerBase<object, AbTestScheduledTriggerOutput, EngageAbTestScheduledNotification>
{
    public AbTestScheduledTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageAbTestScheduledNotification notification)
    {
        yield return new TriggerEvent<AbTestScheduledTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            IdempotencyKey = GenerateIdempotencyKey(notification.Test.Id),
            Output = new AbTestScheduledTriggerOutput
            {
                AbTestId = notification.Test.Id,
                AbTestName = notification.Test.Name,
            },
        };
    }
}
