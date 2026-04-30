using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.customerJourneyStepExplicitScored", "Customer Journey Step Explicitly Assigned",
    Description = "Fires when a visitor is manually locked to a customer journey step.",
    Group = "Engage",
    Icon = "icon-path")]
public sealed class CustomerJourneyStepExplicitScoredTrigger
    : NotificationTriggerBase<object, CustomerJourneyStepExplicitScoredTriggerOutput, EngageCustomerJourneyStepExplicitScoredNotification>
{
    public CustomerJourneyStepExplicitScoredTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCustomerJourneyStepExplicitScoredNotification notification)
    {
        yield return new TriggerEvent<CustomerJourneyStepExplicitScoredTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "system",
            Output = new CustomerJourneyStepExplicitScoredTriggerOutput
            {
                VisitorId = notification.VisitorId,
                CustomerJourneyStepId = notification.Entity.CustomerJourneyStepId,
                GroupId = notification.Entity.GroupId,
            },
        };
    }
}
