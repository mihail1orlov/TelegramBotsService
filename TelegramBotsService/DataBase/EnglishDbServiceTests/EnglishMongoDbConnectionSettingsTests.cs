using EnglishDbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnglishDbServiceTests
{
    [TestClass]
    public class EnglishMongoDbConnectionSettingsTests
    {
        private EnglishMongoDbConnectionSettings _target;

        [TestInitialize]
        public void Init()
        {
            _target = new EnglishMongoDbConnectionSettings();
        }

        [TestMethod]
        public void Port_Test()
        {
            // Arrange
            // Action
            var actual = _target.Port;

            // Assert
            Assert.AreEqual(27017, actual);
        }

        [TestMethod]
        public void Host_Test()
        {
            // Arrange
            // Action
            var actual = _target.Host;

            // Assert
            Assert.AreEqual("localhost", actual);
        }

        [TestMethod]
        public void Database_Test()
        {
            // Arrange
            // Action
            var actual = _target.Database;

            // Assert
            Assert.AreEqual("english_db", actual);
        }
    }
}