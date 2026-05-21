using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.customerJourneyStepExplicitScoreRemoved", "Customer Journey Step Assignment Removed",
    Description = "Fires when a visitor's manual customer journey step assignment is removed.",
    Group = "Engage",
    Icon = "icon-path",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class CustomerJourneyStepExplicitScoreRemovedTrigger
    : NotificationTriggerBase<object, CustomerJourneyStepExplicitScoreRemovedTriggerOutput, EngageCustomerJourneyStepExplicitScoreRemovedNotification>
{
    public CustomerJourneyStepExplicitScoreRemovedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCustomerJourneyStepExplicitScoreRemovedNotification notification)
    {
        yield return new TriggerEvent<CustomerJourneyStepExplicitScoreRemovedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new CustomerJourneyStepExplicitScoreRemovedTriggerOutput
            {
                VisitorId = notification.VisitorId,
                GroupId = notification.GroupId,
                CustomerJourneyStepId = notification.CustomerJourneyStepId,
            },
        };
    }
}
