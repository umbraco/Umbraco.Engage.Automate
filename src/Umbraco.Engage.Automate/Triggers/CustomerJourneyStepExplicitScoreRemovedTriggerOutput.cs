namespace Umbraco.Engage.Automate.Triggers;

public sealed class CustomerJourneyStepExplicitScoreRemovedTriggerOutput
{
    public Guid VisitorId { get; init; }
    public long GroupId { get; init; }
    public long CustomerJourneyStepId { get; init; }
}
