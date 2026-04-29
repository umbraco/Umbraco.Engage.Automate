using Umbraco.Automate.Core.Settings;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>Settings for the <see cref="ScorePersonaAction"/>.</summary>
public sealed class ScorePersonaSettings
{
    [Field(Label = "Visitor External ID", Description = "The visitor's external GUID (e.g., from a trigger output).", SupportsBindings = true)]
    public string VisitorExternalId { get; set; } = string.Empty;

    [Field(Label = "Persona Key", Description = "The GUID key of the persona to score.", SortOrder = 1, SupportsBindings = true)]
    public string PersonaKey { get; set; } = string.Empty;

    [Field(Label = "Score", Description = "Score to add to the persona (positive or negative).", SortOrder = 2, SupportsBindings = true)]
    public int Score { get; set; } = 1;

    [Field(Label = "Lock to Persona", Description = "When enabled, the visitor is explicitly locked to this persona regardless of their calculated score.", SortOrder = 3)]
    public bool IsLocked { get; set; } = false;
}
