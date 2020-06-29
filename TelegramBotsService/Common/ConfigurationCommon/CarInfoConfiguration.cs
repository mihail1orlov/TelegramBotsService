using ConfigurationCommon.Constants;
using Microsoft.Extensions.Configuration;

namespace ConfigurationCommon
{
    public class CarInfoConfiguration : ICarInfoConfiguration
    {
        private readonly IConfigurationRoot _config;

        public CarInfoConfiguration(IConfigurationBuilder configurationBuilder, IFileConstants fileConstants)
        {
            // This is configuration provider
            _config = configurationBuilder.AddJsonFile(fileConstants.ConfigJson).Build();
        }

        public string Token => _config["CarInfoToken"];
    }
}