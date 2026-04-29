namespace Umbraco.Engage.Automate.Triggers;

public sealed class PageviewExtractedTriggerOutput
{
    public long PageviewId { get; init; }
    public Guid PageviewGuid { get; init; }
    public DateTime Timestamp { get; init; }
    public long SessionId { get; init; }
    public bool WasPersonalized { get; init; }
}
