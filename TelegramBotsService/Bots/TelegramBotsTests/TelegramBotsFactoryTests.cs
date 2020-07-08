using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TelegramBots.Entities;
using TelegramBots.Services;
using TelegramBots.Services.Factories;

namespace TelegramBotsTests
{
    /// <summary>
    /// Class that contains tests for <see cref="TelegramBotsFactory"/>
    /// </summary>
    [TestClass]
    public class TelegramBotsFactoryTests
    {
        // Test constants
        private const string TestToken = "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy";
        private const string InvalidToken = "1234567";

        /// <summary>
        /// Target class for test 
        /// </summary>
        private TelegramBotsFactory _telegramBotsFactory;

        /// <summary>
        /// Initializes tests data
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var messageProcessorFactory = Substitute.For<IMessageProcessorFactory>();
            _telegramBotsFactory = new TelegramBotsFactory(messageProcessorFactory);
        }

        /// <summary>
        /// Tests that the telegram bor factory returns the necessary service
        /// </summary>
        /// <param name="telegramBot">Type of telegram bot</param>
        /// <param name="expectedTelegramBot">Expected telegram bot service</param>
        [TestMethod]
        [DataRow(TelegramBot.CarInfoService, typeof(CarInfoService),
            DisplayName = "GetTelegramBot_TokenTelegramBot_CarInfoServiceWasReceived")]
        [DataRow(TelegramBot.EnglishService, typeof(EnglishService),
            DisplayName = "GetTelegramBot_TokenTelegramBot_EnglishServiceWasReceived")]
        public void GetTelegramBot_TokenTelegramBot_NecessaryBotWasReceived(TelegramBot telegramBot,
            Type expectedTelegramBot)
        {
            // Arrange
            // Act
            var actualTelegramBot = _telegramBotsFactory.GetTelegramBot(TestToken, telegramBot);

            // Assert
            Assert.AreEqual(expectedTelegramBot, actualTelegramBot.GetType());
        }

        /// <summary>
        /// Tests that the telegram bot factory throws argument exception
        /// </summary>
        /// <param name="token">The ID of the bot</param>
        /// <param name="telegramBot">Type of telegram bot</param>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(TestToken, TelegramBot.None, 
            DisplayName = "GetTelegramBot_InvalidTelegramBot_ThrowArgumentException")]
        [DataRow(InvalidToken, TelegramBot.CarInfoService,
            DisplayName = "GetTelegramBot_InvalidToken_ThrowArgumentException")]
        public void GetTelegramBot_InvalidParameters_ThrowArgumentException(string token, TelegramBot telegramBot)
        {
            // Arrange
            // Act
            var actualTelegramBot = _telegramBotsFactory.GetTelegramBot(TestToken, TelegramBot.None);
            // Assert
        }
    }
}