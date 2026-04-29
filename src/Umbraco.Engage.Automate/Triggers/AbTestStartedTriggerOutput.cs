namespace Umbraco.Engage.Automate.Triggers;

public sealed class AbTestStartedTriggerOutput
{
    public long AbTestId { get; init; }
    public string? AbTestName { get; init; }
}
