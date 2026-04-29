using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.clientSideGoalCompleted", "Client-Side Goal Completed",
    Description = "Fires when a visitor completes a client-side goal.",
    Group = "Engage",
    Icon = "icon-trophy")]
public sealed class ClientSideGoalCompletedTrigger
    : NotificationTriggerBase<object, ClientSideGoalCompletedTriggerOutput, EngageClientSideGoalCompletedNotification>
{
    public ClientSideGoalCompletedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageClientSideGoalCompletedNotification notification)
    {
        var pageview = notification.Pageview;
        var goal = notification.GoalCompletion;
        yield return new TriggerEvent<ClientSideGoalCompletedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "visitor",
            Output = new ClientSideGoalCompletedTriggerOutput
            {
                PageviewId = pageview.Id,
                PageviewGuid = pageview.Guid,
                GoalId = goal.Goal.Id,
                GoalValue = goal.Value,
                SessionSequenceNumber = notification.SessionSequenceNumber,
                Timestamp = goal.Timestamp,
            },
        };
    }
}
