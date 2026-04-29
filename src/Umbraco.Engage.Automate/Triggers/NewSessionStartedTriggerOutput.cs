namespace Umbraco.Engage.Automate.Triggers;

public sealed class NewSessionStartedTriggerOutput
{
    public long SessionId { get; init; }
    public DateTime SessionTimestamp { get; init; }
    public int PageviewCount { get; init; }
    public long? VisitorId { get; init; }
}
