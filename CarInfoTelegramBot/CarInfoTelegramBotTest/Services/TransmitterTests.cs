using CarInfoCommon.Models;
using CarInfoTelegramBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CarInfoTelegramBotTest.Services
{
    [TestClass]
    public class TransmitterTests
    {
        private ITransmitter _target;

        [TestInitialize]
        public void Init()
        {
            var repository = Substitute.For<IRepository>();
            repository.Load().Returns(new CarInfo(1234));
            _target = new Transmitter(repository);
        }

        [TestMethod]
        public void LoadTest()
        {
            var carInfo = _target.Load();
            Assert.AreEqual(1234, carInfo.Mileage);
        }
    }
}