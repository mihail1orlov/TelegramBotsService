using CarInfoTelegramBotService.Configuration;
using CarInfoTelegramBotService.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CarInfoTelegramBotServiceTests.Configuration
{
    [TestClass]
    public class CarInfoConfigurationTests
    {
        private CarInfoConfiguration _target;

        [TestInitialize]
        public void Init()
        {
            var configurationBuilder = Substitute.For<IConfigurationBuilder>();
            var addJsonFile = Substitute.For<IConfigurationBuilder>();
            var root = Substitute.For<IConfigurationRoot>();
            root["token"].Returns("123321");
            addJsonFile.Build().Returns(root);
            configurationBuilder.AddJsonFile("filePath").Returns(addJsonFile);

            var fileConstants = Substitute.For<IFileConstants>();
            fileConstants.ConfigJson.Returns("filePath");

            _target = new CarInfoConfiguration(configurationBuilder, fileConstants);
        }

        [TestMethod]
        public void CarInfoConfigurationTest()
        {
            var active = _target.Token;
            Assert.AreEqual("", active);
        }
    }
}