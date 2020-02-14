using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBotsTests.Services
{
    [TestClass]
    public class CarInfoServiceTests
    {
        private CarInfoService _target;

        [TestInitialize]
        public void Init()
        {
            var telegramBotClient = Substitute.For<ITelegramBotClient>();
            _target = new CarInfoService(telegramBotClient);
        }

        [TestMethod]
        public void Start_Test()
        {
            _target.Start();
        }

        [TestMethod]
        public void Stop_Test()
        {
            _target.Stop();
        }
    }
}