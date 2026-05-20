namespace Umbraco.Engage.Automate;

/// <summary>
/// Constants for the Engage Automate package.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Umbraco backoffice section aliases referenced by Engage Automate step types.
    /// </summary>
    public static class Sections
    {
        /// <summary>
        /// The Engage section — required for any step type that exposes visitor /
        /// pageview / session data or scores customer-journey, persona, or goal
        /// signals. Pageview-extracted events in particular carry session-level data
        /// the section gate must protect.
        /// </summary>
        public const string Engage = "engage";
    }
}
