namespace Umbraco.Engage.Automate.Triggers;

public sealed class CustomGoalCompletedTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long GoalId { get; init; }
    public int Value { get; init; }
    public DateTime Timestamp { get; init; }
}
