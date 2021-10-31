namespace ConfigurationCommon
{
    /// <summary>
    /// Provides the configuration for the GitHubNotificator bot
    /// </summary>
    public interface IGitHubNotificatorConfiguration
    {
        /// <summary>
        /// Gets the token of telegram bot
        /// </summary>
        string Token { get; }
    }
}