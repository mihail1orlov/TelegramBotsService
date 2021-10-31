namespace ConfigurationCommon
{
    /// <summary>
    /// Provides the configuration for the AvtoCarDrive bot
    /// </summary>
    public interface IAvtoCarDriveConfiguration
    {
        /// <summary>
        /// Gets the token of telegram bot
        /// </summary>
        string Token { get; }
    }
}