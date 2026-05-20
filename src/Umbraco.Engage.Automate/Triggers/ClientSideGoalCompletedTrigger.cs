using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.clientSideGoalCompleted", "Client-Side Goal Completed",
    Description = "Fires when a visitor completes a client-side goal.",
    Group = "Engage",
    Icon = "icon-trophy",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class ClientSideGoalCompletedTrigger
    : NotificationTriggerBase<object, ClientSideGoalCompletedTriggerOutput, EngageClientSideGoalCompletedNotification>
{
    public ClientSideGoalCompletedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageClientSideGoalCompletedNotification notification)
    {
        yield return new TriggerEvent<ClientSideGoalCompletedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new ClientSideGoalCompletedTriggerOutput
            {
                PageviewId = notification.PageviewId,
                PageviewGuid = notification.PageviewGuid,
                GoalId = notification.GoalId,
                GoalValue = notification.GoalValue,
                SessionSequenceNumber = notification.SessionSequenceNumber,
                Timestamp = notification.Timestamp,
            },
        };
    }
}
