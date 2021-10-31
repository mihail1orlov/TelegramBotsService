using System;
using LoggerCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using ILogger = NLog.ILogger;

namespace LoggerCommonTests
{
    [TestClass]
    public class LoggerTests
    {
        private Logger _target;
        private ILogger _logger;

        [TestInitialize]
        public void Init()
        {
            _logger = Substitute.For<ILogger>();
            _target = new Logger(_logger);
        }

        [TestMethod]
        public void ShutdownTest()
        {
            _target.Shutdown();
        }

        [TestMethod]
        public void InfoTest()
        {
            _target.Info("InfoMessage");
            _logger.Received().Info("InfoMessage");
        }

        [TestMethod]
        public void ErrorTest()
        {
            var exception = new AggregateException();
            _target.Error(exception, "ErrorMessage");
            _logger.Received().Error(exception, "ErrorMessage");
        }
    }
}