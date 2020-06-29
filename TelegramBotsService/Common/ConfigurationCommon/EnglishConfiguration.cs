using ConfigurationCommon.Constants;
using Microsoft.Extensions.Configuration;

namespace ConfigurationCommon
{
    public class EnglishConfiguration : IEnglishConfiguration
    {
        private readonly IConfigurationRoot _config;

        public EnglishConfiguration(IConfigurationBuilder configurationBuilder, IFileConstants fileConstants)
        {
            // This is configuration provider
            _config = configurationBuilder.AddJsonFile(fileConstants.ConfigJson).Build();
        }

        public string Token => _config["EnglishToken"];
    }
}