using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.customerJourneyStepScored", "Customer Journey Step Scored",
    Description = "Fires when a visitor's customer journey step score changes.",
    Group = "Engage",
    Icon = "icon-route")]
public sealed class CustomerJourneyStepScoredTrigger
    : NotificationTriggerBase<object, CustomerJourneyStepScoredTriggerOutput, EngageCustomerJourneyStepScoredNotification>
{
    public CustomerJourneyStepScoredTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCustomerJourneyStepScoredNotification notification)
    {
        yield return new TriggerEvent<CustomerJourneyStepScoredTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "visitor",
            IdempotencyKey = GenerateIdempotencyKey(notification.VisitorId, notification.CustomerJourneyStepId, notification.Score),
            Output = new CustomerJourneyStepScoredTriggerOutput
            {
                VisitorId = notification.VisitorId,
                CustomerJourneyStepId = notification.CustomerJourneyStepId,
                Score = notification.Score,
                IsLocked = notification.IsLocked,
            },
        };
    }
}
