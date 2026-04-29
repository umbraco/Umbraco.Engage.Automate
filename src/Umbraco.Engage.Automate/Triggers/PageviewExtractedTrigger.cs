using Umbraco.Automate.Core.Triggers;
using Umbraco.Engage.Automate.Notifications;

namespace Umbraco.Engage.Automate.Triggers;

[Trigger("umbracoEngage.pageviewExtracted", "Pageview Extracted",
    Description = "Fires when a pageview is extracted and processed.",
    Group = "Engage",
    Icon = "icon-document")]
public sealed class PageviewExtractedTrigger
    : NotificationTriggerBase<object, PageviewExtractedTriggerOutput, EngagePageviewExtractedNotification>
{
    public PageviewExtractedTrigger(TriggerInfrastructure infrastructure) : base(infrastructure) { }

    public override IEnumerable<TriggerEvent> MapEvent(EngagePageviewExtractedNotification notification)
    {
        var pageview = notification.Pageview;
        yield return new TriggerEvent<PageviewExtractedTriggerOutput>
        {
            TriggerAlias = Alias,
            InitiatorType = "visitor",
            Output = new PageviewExtractedTriggerOutput
            {
                PageviewId = pageview.Id,
                PageviewGuid = pageview.Guid,
                Timestamp = pageview.Timestamp,
                SessionId = pageview.Session.Id,
                WasPersonalized = pageview.WasPersonalized,
            },
        };
    }
}
