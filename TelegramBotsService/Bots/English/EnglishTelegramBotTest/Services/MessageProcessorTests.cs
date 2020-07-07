using EnglishDbService;
using EnglishTelegramBot.Services;
using LoggerCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EnglishTelegramBotTest.Services
{
    [TestClass]
    public class MessageProcessorTests
    {
        private EnglishMessageProcessor _target;

        [TestInitialize]
        public void Init()
        {
            var englishRepository = Substitute.For<IEnglishRepository>();
            var logger = Substitute.For<ILogger>();
            _target = new EnglishMessageProcessor(englishRepository, logger);
        }

        [TestMethod]
        public void Test()
        {
            var actual = _target.Process("стул", 0).Result;
            Assert.AreEqual("chair", actual);
        }
    }
}