using Autofac;
using CarInfoTelegramBotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarInfoTelegramBotServiceTests
{
    [TestClass]
    public class BootstrapperTests
    {
        private Bootstrapper _target;

        [TestInitialize]
        public void Init()
        {
            _target = new Bootstrapper();
        }

        [TestMethod]
        public void GetDependencyInjectionContainerTest()
        {
            _target.BootStrap(new ContainerBuilder());
        }
    }
}