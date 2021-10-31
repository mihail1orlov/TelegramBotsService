using System;
using AvtoCarDriveBot.Services.AdminIdsServices;
using AvtoCarDriveBot.Services.CarServices;
using AvtoCarDriveBot.Services.MessageProcessorServices;
using LoggerCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Telegram.Bot;

namespace AvtoCarDriveBotTests.Services.MessageProcessorServices
{
    /// <summary>
    /// Class that contains tests for <see cref="MessageProcessorFactory"/>
    /// </summary>
    [TestClass]
    public class MessageProcessorFactoryTests
    {
        // private fields
        private MessageProcessorFactory _target;

        /// <summary>
        /// Setting up the values for variables that will be used in the test
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            var logger = Substitute.For<ILogger>();
            var telegramBotClient = Substitute.For<ITelegramBotClient>(); ;
            var adminIdsService = Substitute.For<IAdminIdsService>();
            var carService = Substitute.For<ICarService>();
            _target = new MessageProcessorFactory(logger, telegramBotClient, adminIdsService, carService);
        }

        /// <summary>
        /// Tests that the GetMessageProcessor returns the necessary message processor
        /// </summary>
        /// <param name="messageProcessorType">Expected type of the message processor</param>
        [TestMethod]
        [DataRow(typeof(AdminMessageProcessor), DisplayName = "GetMessageProcessor_ReturnAdminMessageProcessor")]
        [DataRow(typeof(UserMessageProcessor), DisplayName = "GetMessageProcessor_ReturnUserMessageProcessor")]
        public void GetMessageProcessor_ReturnNecessaryMessageProcessor(Type messageProcessorType)
        {
            // Arrange
            var getMessageProcessor = typeof(MessageProcessorFactory).GetMethod("GetMessageProcessor");
            var genericMethod = getMessageProcessor?.MakeGenericMethod(messageProcessorType);

            // Act
            var actualMessageProcessor = genericMethod?.Invoke(_target, null);

            // Assert
            Assert.IsInstanceOfType(actualMessageProcessor, messageProcessorType);
        }
    }
}