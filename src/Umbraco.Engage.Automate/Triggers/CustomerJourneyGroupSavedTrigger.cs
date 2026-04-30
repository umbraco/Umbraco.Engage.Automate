using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.customerJourneyGroupSaved", "Customer Journey Group Saved",
    Description = "Fires when a customer journey group is saved.",
    Group = "Engage",
    Icon = "icon-path")]
public sealed class CustomerJourneyGroupSavedTrigger
    : NotificationTriggerBase<object, CustomerJourneyGroupSavedTriggerOutput, EngageCustomerJourneyGroupSavedNotification>
{
    public CustomerJourneyGroupSavedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCustomerJourneyGroupSavedNotification notification)
    {
        yield return new TriggerEvent<CustomerJourneyGroupSavedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new CustomerJourneyGroupSavedTriggerOutput(),
        };
    }
}
