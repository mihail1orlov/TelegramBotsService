using System.Collections.Generic;
using Host;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceCommon;

namespace HostTests
{
    [TestClass]
    public class MainServiceTests
    {
        private MainService _target;
        private IService _service;

        [TestInitialize]
        public void Init()
        {
            _service = Substitute.For<IService>();
            var services = new List<IService>
            {
                _service
            };

            _target = new MainService(services);
        }

        [TestMethod]
        public void StartSvcTest()
        {
            _target.StartSvc();
            _service.Received().Start();
        }

        [TestMethod]
        public void StopSvcTest()
        {
            _target.StopSvc();
            _service.Received().Stop();
        }
    }
}