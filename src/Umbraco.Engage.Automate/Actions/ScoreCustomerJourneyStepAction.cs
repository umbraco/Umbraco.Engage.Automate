using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.Core.Runs;
using Umbraco.Engage.Infrastructure.Analytics.Processed;
using Umbraco.Engage.Infrastructure.Personalization.Services;

namespace Umbraco.Engage.Automate.Actions;

/// <summary>
/// Adds a customer journey step score for a visitor, optionally locking them to that step.
/// </summary>
[Action("umbracoEngage.scoreCustomerJourneyStep", "Score Customer Journey Step",
    Description = "Adds a customer journey step score for a visitor. Set IsLocked to explicitly assign the visitor to the step.",
    Group = "Engage",
    Icon = "icon-route")]
public sealed class ScoreCustomerJourneyStepAction : ActionBase<ScoreCustomerJourneyStepSettings, ScoreCustomerJourneyStepOutput>
{
    private readonly ICustomerJourneyService _customerJourneyService;

    public ScoreCustomerJourneyStepAction(ActionInfrastructure infrastructure, ICustomerJourneyService customerJourneyService)
        : base(infrastructure) => _customerJourneyService = customerJourneyService;

    public override Task<ActionResult> ExecuteAsync(ActionContext context, CancellationToken cancellationToken)
    {
        var settings = context.GetSettings<ScoreCustomerJourneyStepSettings>();

        if (!Guid.TryParse(settings.VisitorExternalId, out var visitorExternalId))
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("A valid Visitor External ID (GUID) is required."),
                StepRunErrorCategory.Validation));
        }

        if (!Guid.TryParse(settings.StepKey, out var stepKey))
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("A valid Customer Journey Step Key (GUID) is required."),
                StepRunErrorCategory.Validation));
        }

        if (settings.Score == 0 && !settings.IsLocked)
        {
            return Task.FromResult(ActionResult.Failed(
                new ArgumentException("Score must be non-zero, or IsLocked must be true for an explicit assignment."),
                StepRunErrorCategory.Validation));
        }

        _customerJourneyService.ScoreCustomerJourneyStep(
            visitorExternalId,
            stepKey,
            settings.Score,
            goalId: 0,
            PersonalizationScoreType.Custom,
            settings.IsLocked);

        return Task.FromResult(Success(new ScoreCustomerJourneyStepOutput
        {
            VisitorExternalId = visitorExternalId,
            StepKey = stepKey,
            Score = settings.Score,
            IsLocked = settings.IsLocked,
        }));
    }
}
