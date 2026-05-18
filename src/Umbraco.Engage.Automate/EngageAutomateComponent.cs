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
    // High-volume visitor-event triggers excluded from initial launch — see EngageAutomateComposer.
    // NewSessionStartedBridgeHandler newSessionStarted,
    // PageviewExtractedBridgeHandler pageviewExtracted,
    AppliedPersonalizationSavedBridgeHandler appliedPersonalizationSaved,
    SegmentSavedBridgeHandler segmentSaved,
    SegmentDeletedBridgeHandler segmentDeleted,
    CustomerJourneyGroupSavedBridgeHandler customerJourneyGroupSaved,
    // CustomerJourneyStepScoredBridgeHandler customerJourneyStepScored,
    CustomerJourneyStepExplicitScoredBridgeHandler customerJourneyStepExplicitScored,
    CustomerJourneyStepExplicitScoreRemovedBridgeHandler customerJourneyStepExplicitScoreRemoved,
    PersonaGroupSavedBridgeHandler personaGroupSaved,
    // PersonaScoredBridgeHandler personaScored,
    PersonaExplicitScoredBridgeHandler personaExplicitScored,
    PersonaExplicitScoreRemovedBridgeHandler personaExplicitScoreRemoved,
    GoalsSavedBridgeHandler goalsSaved,
    // CustomGoalCompletedBridgeHandler customGoalCompleted,
    // ClientSideGoalCompletedBridgeHandler clientSideGoalCompleted,
    CampaignGroupSavedBridgeHandler campaignGroupSaved) : IComponent
{
    public void Initialize()
    {
        abTestSaved.Register();
        abTestScheduled.Register();
        abTestStarted.Register();
        abTestStopped.Register();
        abTestVariantSaved.Register();
        // newSessionStarted.Register();
        // pageviewExtracted.Register();
        appliedPersonalizationSaved.Register();
        segmentSaved.Register();
        segmentDeleted.Register();
        customerJourneyGroupSaved.Register();
        // customerJourneyStepScored.Register();
        customerJourneyStepExplicitScored.Register();
        customerJourneyStepExplicitScoreRemoved.Register();
        personaGroupSaved.Register();
        // personaScored.Register();
        personaExplicitScored.Register();
        personaExplicitScoreRemoved.Register();
        goalsSaved.Register();
        // customGoalCompleted.Register();
        // clientSideGoalCompleted.Register();
        campaignGroupSaved.Register();
    }

    public void Terminate()
    {
        abTestSaved.Unregister();
        abTestScheduled.Unregister();
        abTestStarted.Unregister();
        abTestStopped.Unregister();
        abTestVariantSaved.Unregister();
        // newSessionStarted.Unregister();
        // pageviewExtracted.Unregister();
        appliedPersonalizationSaved.Unregister();
        segmentSaved.Unregister();
        segmentDeleted.Unregister();
        customerJourneyGroupSaved.Unregister();
        // customerJourneyStepScored.Unregister();
        customerJourneyStepExplicitScored.Unregister();
        customerJourneyStepExplicitScoreRemoved.Unregister();
        personaGroupSaved.Unregister();
        // personaScored.Unregister();
        personaExplicitScored.Unregister();
        personaExplicitScoreRemoved.Unregister();
        goalsSaved.Unregister();
        // customGoalCompleted.Unregister();
        // clientSideGoalCompleted.Unregister();
        campaignGroupSaved.Unregister();
    }
}
