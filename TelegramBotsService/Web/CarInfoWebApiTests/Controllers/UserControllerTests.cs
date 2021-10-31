using System;
using System.Linq;
using CarInfoWebApi.Controllers;
using Xunit;

namespace CarInfoWebApiTests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _target;

        public UserControllerTests()
        {
            _target = new UserController();
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange
            // Act
            var actual = _target.Get();
            var collection = new []{ "value1", "value2" };

            // Assert
            Assert.Equal(collection, actual.ToArray());
        }

        [Fact]
        public void GetTest()
        {
            // Arrange
            // Act
            var actual = _target.Get(1);

            // Assert
            Assert.Equal("value", actual);
        }

        [Fact]
        public void PostTest()
        {
            // Arrange
            // Act
            Action action = () => _target.Post("");

            // Assert
            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void PutTest()
        {
            // Arrange
            // Act
            Action action = () =>_target.Put(1, "");

            // Assert
            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            // Act
            Action action = () => _target.Delete(1);

            // Assert
            Assert.Throws<NotImplementedException>(action);
        }
    }
}