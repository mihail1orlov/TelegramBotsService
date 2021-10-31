using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDbCommon;
using NSubstitute;

namespace MongoDbCommonTests
{
    #region mock
    public class MongoMockRepositoryMock : MongoRepository<ModelMock, string>, IModelMockRepository
    {
        public MongoMockRepositoryMock(IMongoClient client, IConnectionSettings connectionSettings, string collectionName)
            : base(client, connectionSettings, collectionName) { }
    }

    public interface IModelMockRepository { }

    public class ModelMock : IDbEntity<string>
    {
        public string Id { get; set; }
    }
    #endregion

    [TestClass]
    public class MongoRepositoryTests
    {
        private MongoMockRepositoryMock _target;

        [TestInitialize]
        public void Init()
        {
            var connectionSettings = Substitute.For<IConnectionSettings>();
            connectionSettings.Database.Returns("DataBaseTest");

            var client = Substitute.For<IMongoClient>();
            var dataBase = Substitute.For<IMongoDatabase>();
            var collection = Substitute.For<IMongoCollection<ModelMock>>();

            dataBase.GetCollection<ModelMock>("collectionNameTest").Returns(collection);

            client.GetDatabase(connectionSettings.Database).Returns(dataBase);

            _target = new MongoMockRepositoryMock(client, connectionSettings, "collectionNameTest");
        }

        [TestMethod]
        public void Save_Test()
        {
            // Arrange
            // Action
            // Assert
            var actual = _target.Save(new ModelMock{Id = "IdMock"});
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
            var actual = _target.FindAll<ModelMock>(x => true).ToArray();
            
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
            _target.FindAll<ModelMock>(null);
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
        public void Get_Test()
        {
            // Arrange
            // Action
            var actual = _target.Get("");
            
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
            Assert.AreEqual(nameof(ModelMock), actual.ElementType.Name);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            // Arrange
            // Action
            var actual = _target.GetAll().ToArray();
            
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
            _target = new MongoMockRepositoryMock(null, null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "connectionSettings")]
        public void Ctor_Exception_Test()
        {
            // Arrange
            var client = Substitute.For<IMongoClient>();

            // Action
            // Assert
            _target = new MongoMockRepositoryMock(client, null, "");
        }

        [TestMethod]
        public void Delete_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete(new ModelMock {Id = "Id"});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ModelMock")]
        public void Delete_nullModel_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete((ModelMock)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "id")]
        public void Delete_nullId_Test()
        {
            // Arrange
            // Action
            // Assert
            _target.Delete(new ModelMock());
        }
    }
}