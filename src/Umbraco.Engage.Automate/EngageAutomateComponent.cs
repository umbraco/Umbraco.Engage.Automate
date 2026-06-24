using Umbraco.Cms.Core.Composing;
using Umbraco.Engage.Automate.Notifications.Handlers;
using Umbraco.Engage.Infrastructure.Events;

namespace Umbraco.Engage.Automate;

/// <summary>
/// Registers and unregisters all bridge handlers with Engage's SystemEventService
/// during the Umbraco component lifecycle.
/// </summary>
internal sealed class EngageAutomateComponent(
    AbTestSavedBridgeHandler abTestSaved,
    AbTestScheduledBridgeHandler abTestScheduled,
    AbTestStartedBridgeHandler abTestStarted,
    AbTestStoppedBridgeHandler abTestStopped,
    AbTestVariantSavedBridgeHandler abTestVariantSaved,
    AppliedPersonalizationSavedBridgeHandler appliedPersonalizationSaved,
    SegmentSavedBridgeHandler segmentSaved,
    SegmentDeletedBridgeHandler segmentDeleted,
    CustomerJourneyGroupSavedBridgeHandler customerJourneyGroupSaved,
    CustomerJourneyStepExplicitScoredBridgeHandler customerJourneyStepExplicitScored,
    CustomerJourneyStepExplicitScoreRemovedBridgeHandler customerJourneyStepExplicitScoreRemoved,
    PersonaGroupSavedBridgeHandler personaGroupSaved,
    PersonaExplicitScoredBridgeHandler personaExplicitScored,
    PersonaExplicitScoreRemovedBridgeHandler personaExplicitScoreRemoved,
    GoalsSavedBridgeHandler goalsSaved,
    CampaignGroupSavedBridgeHandler campaignGroupSaved) : IAsyncComponent
{
    public Task InitializeAsync(bool isRestarting, CancellationToken cancellationToken)
    {
        abTestSaved.Register();
        abTestScheduled.Register();
        abTestStarted.Register();
        abTestStopped.Register();
        abTestVariantSaved.Register();
        appliedPersonalizationSaved.Register();
        segmentSaved.Register();
        segmentDeleted.Register();
        customerJourneyGroupSaved.Register();
        customerJourneyStepExplicitScored.Register();
        customerJourneyStepExplicitScoreRemoved.Register();
        personaGroupSaved.Register();
        personaExplicitScored.Register();
        personaExplicitScoreRemoved.Register();
        goalsSaved.Register();
        campaignGroupSaved.Register();

        return Task.CompletedTask;
    }

    public Task TerminateAsync(bool isRestarting, CancellationToken cancellationToken)
    {
        abTestSaved.Unregister();
        abTestScheduled.Unregister();
        abTestStarted.Unregister();
        abTestStopped.Unregister();
        abTestVariantSaved.Unregister();
        appliedPersonalizationSaved.Unregister();
        segmentSaved.Unregister();
        segmentDeleted.Unregister();
        customerJourneyGroupSaved.Unregister();
        customerJourneyStepExplicitScored.Unregister();
        customerJourneyStepExplicitScoreRemoved.Unregister();
        personaGroupSaved.Unregister();
        personaExplicitScored.Unregister();
        personaExplicitScoreRemoved.Unregister();
        goalsSaved.Unregister();
        campaignGroupSaved.Unregister();

        return Task.CompletedTask;
    }
}
