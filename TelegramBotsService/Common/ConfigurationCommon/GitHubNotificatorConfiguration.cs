using ConfigurationCommon.Constants;
using Microsoft.Extensions.Configuration;

namespace ConfigurationCommon
{
    /// <summary>
    /// Provides the configuration for the GitHubNotificator bot
    /// </summary>
    public class GitHubNotificatorConfiguration : IGitHubNotificatorConfiguration
    {
        // Private fields
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// Creates instance of <see cref="GitHubNotificatorConfiguration"/>
        /// </summary>
        /// <param name="configurationBuilder">Represents a type used to build application configuration</param>
        /// <param name="fileConstants">Provides constants</param>
        public GitHubNotificatorConfiguration(IConfigurationBuilder configurationBuilder, IFileConstants fileConstants)
        {
            // This is configuration provider
            _config = configurationBuilder.AddJsonFile(fileConstants.ConfigJson).Build();
        }

        /// <summary>
        /// Gets the token of telegram bot
        /// </summary>
        public string Token => _config["GitHubNotificatorToken"];
    }
}