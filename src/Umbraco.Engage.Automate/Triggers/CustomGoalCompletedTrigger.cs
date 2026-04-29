using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.customGoalCompleted", "Custom Goal Completed",
    Description = "Fires when a visitor completes a custom goal.",
    Group = "Engage",
    Icon = "icon-trophy")]
public sealed class CustomGoalCompletedTrigger
    : NotificationTriggerBase<object, CustomGoalCompletedTriggerOutput, EngageCustomGoalCompletedNotification>
{
    public CustomGoalCompletedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngageCustomGoalCompletedNotification notification)
    {
        yield return new TriggerEvent<CustomGoalCompletedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "visitor",
            IdempotencyKey = GenerateIdempotencyKey(notification.VisitorId, notification.GoalId, notification.Timestamp),
            Output = new CustomGoalCompletedTriggerOutput
            {
                VisitorId = notification.VisitorId,
                GoalId = notification.GoalId,
                Value = notification.Value,
                Timestamp = notification.Timestamp,
            },
        };
    }
}
