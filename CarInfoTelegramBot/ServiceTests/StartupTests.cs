using CarInfoTelegramBotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace CarInfoTelegramBotServiceTests
{
    [TestClass]
    public class StartupTests
    {
        private Startup _target;

        [TestInitialize]
        public void Init()
        {
            _target = new Startup();
        }

        [TestMethod]
        public void MainTest()
        {
            Startup.Main(new string []{});
        }
    }
}