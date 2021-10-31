using ConfigurationCommon.Constants;
using Microsoft.Extensions.Configuration;

namespace ConfigurationCommon
{
    /// <summary>
    /// Provides the configuration for the AvtoCarDrive bot
    /// </summary>
    public class AvtoCarDriveConfiguration : IAvtoCarDriveConfiguration
    {
        // Private fields
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// Creates instance of <see cref="AvtoCarDriveConfiguration"/>
        /// </summary>
        /// <param name="configurationBuilder">Represents a type used to build application configuration</param>
        /// <param name="fileConstants">Provides constants</param>
        public AvtoCarDriveConfiguration(IConfigurationBuilder configurationBuilder, IFileConstants fileConstants)
        {
            // This is configuration provider
            _config = configurationBuilder.AddJsonFile(fileConstants.ConfigJson).Build();
        }

        /// <summary>
        /// Gets the token of telegram bot
        /// </summary>
        public string Token => _config["AvtoCarDriveToken"];
    }
}