namespace Umbraco.Engage.Automate.Actions;

/// <summary>Output produced by the <see cref="ScoreCustomerJourneyStepAction"/>.</summary>
public sealed class ScoreCustomerJourneyStepOutput
{
    public Guid VisitorExternalId { get; init; }
    public Guid StepKey { get; init; }
    public int Score { get; init; }
    public bool IsLocked { get; init; }
}
