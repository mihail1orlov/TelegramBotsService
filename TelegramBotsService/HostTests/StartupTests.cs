using Microsoft.VisualStudio.TestTools.UnitTesting;
using Host;

namespace HostTests
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
            // todo: need to fix
            // Startup.Main(new string []{});
        }
    }
}