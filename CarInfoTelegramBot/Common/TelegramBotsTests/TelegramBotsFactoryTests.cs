using CarInfoTelegramBot.Services;
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
            var receiver = Substitute.For<IReceiver>();
            var transmitter = Substitute.For<ITransmitter>();
            _target = new TelegramBotsFactory(receiver, transmitter);
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