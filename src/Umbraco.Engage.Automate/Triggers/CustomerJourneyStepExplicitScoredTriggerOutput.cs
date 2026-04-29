namespace Umbraco.Engage.Automate.Triggers;

public sealed class CustomerJourneyStepExplicitScoredTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long CustomerJourneyStepId { get; init; }
    public long GroupId { get; init; }
}
