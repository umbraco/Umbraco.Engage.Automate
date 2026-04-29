namespace Umbraco.Engage.Automate.Triggers;

public sealed class ClientSideGoalCompletedTriggerOutput
{
    public long PageviewId { get; init; }
    public Guid PageviewGuid { get; init; }
    public long GoalId { get; init; }
    public decimal GoalValue { get; init; }
    public int SessionSequenceNumber { get; init; }
    public DateTime Timestamp { get; init; }
}
