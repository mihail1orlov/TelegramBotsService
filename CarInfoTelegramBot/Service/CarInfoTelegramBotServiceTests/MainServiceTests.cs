using System.Collections.Generic;
using CarInfoTelegramBotService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceCommon;

namespace CarInfoTelegramBotServiceTests
{
    [TestClass]
    public class MainServiceTests
    {
        private class MainServiceFake : MainService
        {
            public MainServiceFake(IEnumerable<IService> services) : base(services)
            {
            }

            public new void OnStart(string[] args)
            {
                base.OnStart(args);
            }

            public new void OnStop()
            {
                base.OnStop();
            }
        }

        private MainServiceFake _target;
        private IService _service;

        [TestInitialize]
        public void Init()
        {
            _service = Substitute.For<IService>();
            _target = new MainServiceFake(new List<IService>
            {
                _service
            });
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
        
        [TestMethod]
        public void OnStartTest()
        {
            _target.OnStart(new string []{});
            _service.Received().Start();
        }

        [TestMethod]
        public void OnStopTest()
        {
            _target.OnStop();
            _service.Received().Stop();
        }
    }
}