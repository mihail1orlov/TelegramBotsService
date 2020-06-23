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
            var carInfoMessageProcessor = Substitute.For<CarInfoTelegramBot.Services.IMessageProcessor>();
            var englishMessageProcessor = Substitute.For<EnglishTelegramBot.Services.IMessageProcessor>();
            _target = new TelegramBotsFactory(carInfoMessageProcessor, englishMessageProcessor);
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