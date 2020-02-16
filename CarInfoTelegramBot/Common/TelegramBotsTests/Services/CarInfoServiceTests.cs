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
        private ITelegramBotClient _telegramBotClient;

        [TestInitialize]
        public void Init()
        {
            _telegramBotClient = Substitute.For<ITelegramBotClient>();
            _target = new CarInfoService(_telegramBotClient);
        }

        [TestMethod]
        public void Start_Test()
        {
            _target.Start();
            _telegramBotClient.Received().StartReceiving();
        }

        [TestMethod]
        public void Stop_Test()
        {
            _target.Stop();
            _telegramBotClient.Received().StopReceiving();
        }
    }
}