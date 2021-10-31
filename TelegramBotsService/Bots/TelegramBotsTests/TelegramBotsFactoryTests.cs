using LoggerCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TelegramBots;
using TelegramBots.Services;

namespace TelegramBotsTests
{
    /// <summary>
    /// Class that contains tests for <see cref="TelegramBotsFactory"/>
    /// </summary>
    [TestClass]
    public class TelegramBotsFactoryTests
    {
        // Private fields
        private TelegramBotsFactory _target;
        private const string TestToken = "1006715326:AAFIkPuAG8GO6cv8pVnPK7gKe7ztBDRFMhM";

        /// <summary>
        /// Setting up the values for variables that will be used in the test
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var carInfoMessageProcessor = Substitute.For<CarInfoTelegramBot.Services.IMessageProcessor>();
            var englishMessageProcessor = Substitute.For<EnglishTelegramBot.Services.IMessageProcessor>();
            var logger = Substitute.For<ILogger>();
            _target = new TelegramBotsFactory(carInfoMessageProcessor, englishMessageProcessor, logger);
        }

        /// <summary>
        /// Tests that the TelegramBotsFactory creates CarInfoService
        /// </summary>
        [TestMethod]
        public void GetCarInfoServiceTest_ReturnCarInfoService()
        {
            // Arrange
            // Act
            var actual = _target.GetCarInfoService(TestToken);

            // Assert
            Assert.AreEqual(typeof(CarInfoService), actual.GetType());
        }

        /// <summary>
        /// Tests that the TelegramBotsFactory creates EnglishService
        /// </summary>
        [TestMethod]
        public void GetEnglishService_ReturnEnglishServiceService()
        {
            // Arrange
            // Act
            var actual = _target.GetEnglishService(TestToken);

            // Assert
            Assert.AreEqual(typeof(EnglishService), actual.GetType());
        }

        /// <summary>
        /// Tests that the TelegramBotsFactory creates GitHubNotificatorService
        /// </summary>
        [TestMethod]
        public void GetGitHubNotificatorService_ReturnGitHubNotificatorService()
        {
            // Arrange
            // Act
            var actual = _target.GetGitHubNotificatorService(TestToken);

            // Assert
            Assert.AreEqual(typeof(GitHubNotificatorService), actual.GetType());
        }

        /// <summary>
        /// Tests that the TelegramBotsFactory creates AvtoCarDriveService
        /// </summary>
        [TestMethod]
        public void GetAvtoCarDriveService_ReturnAvtoCarDriveService()
        {
            // Arrange
            // Act
            var actual = _target.GetAvtoCarDriveService(TestToken);

            // Assert
            Assert.AreEqual(typeof(AvtoCarDriveService), actual.GetType());
        }
    }
}