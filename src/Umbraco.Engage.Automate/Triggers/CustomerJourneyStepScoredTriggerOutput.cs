namespace Umbraco.Engage.Automate.Triggers;

public sealed class CustomerJourneyStepScoredTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long CustomerJourneyStepId { get; init; }
    public int Score { get; init; }
    public bool IsLocked { get; init; }
}
