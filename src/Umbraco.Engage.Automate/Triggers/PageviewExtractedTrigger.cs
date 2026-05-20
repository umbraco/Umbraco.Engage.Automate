using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.pageviewExtracted", "Pageview Extracted",
    Description = "Fires when a pageview is extracted and processed.",
    Group = "Engage",
    Icon = "icon-document",
    RequiredSections = [Constants.Sections.Engage])]
public sealed class PageviewExtractedTrigger
    : NotificationTriggerBase<object, PageviewExtractedTriggerOutput, EngagePageviewExtractedNotification>
{
    public PageviewExtractedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePageviewExtractedNotification notification)
    {
        yield return new TriggerEvent<PageviewExtractedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = TriggerInitiatorType.System,
            Output = new PageviewExtractedTriggerOutput
            {
                PageviewId = notification.PageviewId,
                PageviewGuid = notification.PageviewGuid,
                Timestamp = notification.Timestamp,
                SessionId = notification.SessionId,
                WasPersonalized = notification.WasPersonalized,
            },
        };
    }
}
