using CarInfoCommon.Models;
using CarInfoTelegramBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CarInfoTelegramBotTest.Services
{
    [TestClass]
    public class ReceiverTests
    {
        private IReceiver _target;

        [TestInitialize]
        public void Init()
        {
            var repository = Substitute.For<IRepository>();
            _target = new Receiver(repository);
        }

        [TestMethod]
        public void MessageTest()
        {
            Assert.IsTrue(_target.Message(new CarInfo(1234)));
        }
    }
}