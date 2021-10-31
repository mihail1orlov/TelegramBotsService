using System;
using System.Linq;
using EnglishCommon.Models;
using EnglishDbService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDbCommon;
using NSubstitute;

namespace EnglishDbServiceTests
{
    [TestClass]
    public class EnglishRepositoryTests
    {
        private EnglishRepository _target;

        [TestInitialize]
        public void Init()
        {
            var connectionSettings = Substitute.For<IConnectionSettings>();
            connectionSettings.Database.Returns("DataBaseTest");

            var client = Substitute.For<IMongoClient>();
            var dataBase = Substitute.For<IMongoDatabase>();
            var collection = Substitute.For<IMongoCollection<EnglishExercise>>();

            dataBase.GetCollection<EnglishExercise>("collectionNameTest").Returns(collection);

            client.GetDatabase(connectionSettings.Database).Returns(dataBase);

            _target = new EnglishRepository(client, connectionSettings);
        }

        [TestMethod]
        public void Save_Test()
        {
            // Arrange
            // Action
            // Assert
            var actual = _target.Save(new EnglishExercise { Id = "IdMock" });
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
            var actual = _target.FindAll<EnglishExercise>(x => true).ToArray();

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
            _target.FindAll<EnglishExercise>(null);
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
        public void GetEnglishExercise_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetEnglishExercise("");

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "id")]
        public void GetEnglishExercise_Exception_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetEnglishExercise(null);

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
            Assert.AreEqual(nameof(EnglishExercise), actual.ElementType.Name);
        }

        [TestMethod]
        public void GetExercises_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetExercises().ToArray();

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
            _target = new EnglishRepository(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "connectionSettings")]
        public void Ctor_Exception_Test()
        {
            // Arrange
            var client = Substitute.For<IMongoClient>();

            // Action
            // Assert
            _target = new EnglishRepository(client, null);
        }

        [TestMethod]
        public void Delete_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete(new EnglishExercise { Id = "Id" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "EnglishExercise")]
        public void Delete_nullModel_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete((EnglishExercise)null);
        }
    }
}