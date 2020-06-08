using BotCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TelegramBots;

namespace TelegramBotsTests
{
    [TestClass]
    public class TelegramBotsFactoryTests
    {
        private TelegramBotsFactory _target;

        [TestInitialize]
        public void Init()
        {
            var messageProcessor = Substitute.For<IMessageProcessor>();
            _target = new TelegramBotsFactory(messageProcessor);
        }

        [TestMethod]
        public void GetCarInfoServiceTest()
        {
            // todo: incorrect implementation gives to incorrect test
            var actual = _target.GetCarInfoService("token");
            Assert.IsNotNull(actual);
        }
    }
}