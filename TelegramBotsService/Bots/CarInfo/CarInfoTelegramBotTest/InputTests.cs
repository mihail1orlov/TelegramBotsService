using CarInfoTelegramBot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarInfoTelegramBotTest
{
    [TestClass]
    public class InputTests
    {
        private Input _target;

        [TestInitialize]
        public void Init()
        {
            _target = new Input();
        }

        [TestMethod]
        public void SetMileageTest()
        {
            _target.SetMileage(186019);
        }
    }
}