using CarInfoTelegramBot;
using CarInfoTelegramBot.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarInfoTelegramBotTest
{
    [TestClass]
    public class FileWriterTest
    {
        private FileWriter _target;

        [TestInitialize]
        public void Init()
        {
            _target = new FileWriter();
        }

        [TestMethod]
        public void Test()
        {
            int mileage = 186019;
            var carInfo = new CarInfo(mileage);
            _target.Write(carInfo);
        }
    }
}