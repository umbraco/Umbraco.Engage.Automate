using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Personalization.Services;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>
/// Adds a persona score for a visitor, optionally locking them to that persona.
/// </summary>
[Action("umbracoEngage.scorePersona", "Score Persona",
    Description = "Adds a persona score for a visitor. Set IsLocked to explicitly assign the visitor to the persona.",
    Group = "Engage",
    Icon = "icon-user",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class ScorePersonaAction : ActionBase<ScorePersonaSettings, ScorePersonaOutput>
{
    private readonly IPersonaService _personaService;

    public ScorePersonaAction(ActionInfrastructure infrastructure, IPersonaService personaService)
        : base(infrastructure) => _personaService = personaService;

    public override Task<ActionResult> ExecuteAsync(ActionContext context, CancellationToken cancellationToken)
    {
        var settings = context.GetSettings<ScorePersonaSettings>();

        if (!Guid.TryParse(settings.VisitorExternalId, out var visitorExternalId))
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("A valid Visitor External ID (GUID) is required."),
                StepRunErrorCategory.Validation));
        }

        if (!Guid.TryParse(settings.PersonaKey, out var personaKey))
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("A valid Persona Key (GUID) is required."),
                StepRunErrorCategory.Validation));
        }

        if (settings.Score == 0 && !settings.IsLocked)
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("Score must be non-zero, or IsLocked must be true for an explicit assignment."),
                StepRunErrorCategory.Validation));
        }

        _personaService.ScorePersona(
            visitorExternalId,
            personaKey,
            settings.Score,
            goalId: 0,
            PersonalizationScoreType.Custom,
            settings.IsLocked);

        return Task.FromResult(Success(new ScorePersonaOutput
        {
            VisitorExternalId = visitorExternalId,
            PersonaKey = personaKey,
            Score = settings.Score,
            IsLocked = settings.IsLocked,
        }));
    }
}
