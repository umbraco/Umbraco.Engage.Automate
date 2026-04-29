using Umbraco.Automate.Core.Settings;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>Settings for the <see cref="ScoreCustomerJourneyStepAction"/>.</summary>
public sealed class ScoreCustomerJourneyStepSettings
{
    [Field(Label = "Visitor External ID", Description = "The visitor's external GUID (e.g., from a trigger output).", SupportsBindings = true)]
    public string VisitorExternalId { get; set; } = string.Empty;

    [Field(Label = "Step Key", Description = "The GUID key of the customer journey step to score.", SortOrder = 1, SupportsBindings = true)]
    public string StepKey { get; set; } = string.Empty;

    [Field(Label = "Score", Description = "Score to add to the customer journey step (positive or negative).", SortOrder = 2, SupportsBindings = true)]
    public int Score { get; set; } = 1;

    [Field(Label = "Lock to Step", Description = "When enabled, the visitor is explicitly locked to this customer journey step.", SortOrder = 3)]
    public bool IsLocked { get; set; } = false;
}
