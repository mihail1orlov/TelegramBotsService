using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceCommon;
using Telegram.Bot;
using TelegramBots;
using TelegramBots.Services;

namespace TelegramBotsTests
{
    [TestClass]
    public class TelegramBotsFactoryTests
    {
        private TelegramBotsFactory _target;

        [TestInitialize]
        public void Init()
        {
            _target = new TelegramBotsFactory();
        }

        [TestMethod]
        public void GetCarInfoServiceTest()
        {
            // todo: incorrect implementation gives to incorrect test
            var actual = _target.GetCarInfoService("token");
            IService expected = new CarInfoService(new TelegramBotClient("token"));
            Assert.AreEqual(expected, actual);
        }
    }
}