using System;
using System.Linq;
using CarInfoCommon.Models;
using CarInfoDbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDbCommon;
using NSubstitute;

namespace CarInfoDbServiceTests
{
    [TestClass]
    public class MongoDbConnectionSettingsTests
    {
        private MongoDbConnectionSettings _target;

        [TestInitialize]
        public void Init()
        {
            _target = new MongoDbConnectionSettings();
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
            Assert.AreEqual("carInfo_db", actual);
        }
    }

    [TestClass]
    public class CarInfoRepositoryTests
    {
        private CarInfoRepository _target;

        [TestInitialize]
        public void Init()
        {
            var connectionSettings = Substitute.For<IConnectionSettings>();
            connectionSettings.Database.Returns("DataBaseTest");

            var client = Substitute.For<IMongoClient>();
            var dataBase = Substitute.For<IMongoDatabase>();
            var collection = Substitute.For<IMongoCollection<CarInfo>>();

            dataBase.GetCollection<CarInfo>("collectionNameTest").Returns(collection);

            client.GetDatabase(connectionSettings.Database).Returns(dataBase);

            _target = new CarInfoRepository(client, connectionSettings);
        }

        [TestMethod]
        public void Save_Test()
        {
            // Arrange
            // Action
            // Assert
            var actual = _target.Save(new CarInfo(111) { Id = "IdMock" });
            Assert.AreEqual("IdMock", actual.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_Exception_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Save(null);
        }

        [TestMethod]
        public void Find_Test()
        {
            // Arrange
            // Action
            var actual = _target.Find(x => true);

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Find_Exception_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Find(null);
        }

        [TestMethod]
        public void FindAllGeneric_Test()
        {
            // Arrange
            // Action
            var actual = _target.FindAll<CarInfo>(x => true).ToArray();

            // Assert
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindAllGeneric_Exception_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.FindAll<CarInfo>(null);
        }

        [TestMethod]
        public void FindAll_Test()
        {
            // Arrange
            // Action
            var actual = _target.FindAll("x").ToArray();

            // Assert
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindAll_Exception_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.FindAll("");
        }

        [TestMethod]
        public void GetCarInfo_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetCarInfo("");

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "id")]
        public void Get_Exception_Test()
        {
            // Arrange
            // Action
            var actual = _target.Get(null);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void AsQueryable_Test()
        {
            // Arrange
            // Action
            var actual = _target.AsQueryable();

            // Assert
            Assert.AreEqual(nameof(CarInfo), actual.ElementType.Name);
        }

        [TestMethod]
        public void GetCarInfos_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetCarInfos().ToArray();

            // Assert
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "client")]
        public void Ctor_ClientException_Test()
        {
            // Arrange
            // Action
            // Assert
            _target = new CarInfoRepository(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "connectionSettings")]
        public void Ctor_Exception_Test()
        {
            // Arrange
            var client = Substitute.For<IMongoClient>();

            // Action
            // Assert
            _target = new CarInfoRepository(client, null);
        }

        [TestMethod]
        public void Delete_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete(new CarInfo(222) { Id = "Id" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CarInfo")]
        public void Delete_nullModel_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete((CarInfo)null);
        }
    }
}