using System;
using System.Collections.Generic;
using System.IO;
using AvtoCarDriveBot.Services;
using AvtoCarDriveBot.Services.AdminIdsServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvtoCarDriveBotTests.Services
{
    /// <summary>
    /// Class that contains integration tests for <see cref="AdminIdsService"/>
    /// </summary>
    [TestClass]
    public class AdminIdsServiceTests
    {
        // private fields
        private AdminIdsService _target;
        private static readonly string PathApp = Environment.CurrentDirectory + "/TestData/AdminIds.txt";

        /// <summary>
        /// Setting up the values for variables that will be used in the test
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            RemoveDataFile();

            _target = new AdminIdsService(Environment.CurrentDirectory + "/TestData");
        }

        /// <summary>
        /// Cleans up resources after test is run
        /// </summary>
        [TestCleanup]
        public void CleanupTest()
        {
            RemoveDataFile();
        }

        /// <summary>
        /// Tests that the admin was successfully added
        /// </summary>
        [TestMethod]
        public void SetAdminId_AdminWasAdded()
        {
            // Arrange
            var expectedListOfAdmins = new List<long>
            {
                111111
            };

            // Act
            var adminWasAdded = _target.SetAdminId(111111);
            var actualListOfAdmin = _target.AdminIds;

            // Assert
            Assert.IsTrue(adminWasAdded);
            CollectionAssert.AreEqual(expectedListOfAdmins, actualListOfAdmin);
        }

        /// <summary>
        /// Tests that the admin was successfully removed
        /// </summary>
        [TestMethod]
        public void RemoveAdminId_AdminWasRemoved()
        {
            // Arrange
            _target.SetAdminId(111111);
            _target.SetAdminId(222222);
            _target.SetAdminId(333333);
            _target.SetAdminId(444444);

            var expectedListOfAdmins = new List<long>
            {
                111111,
                222222,
                333333
            };

            // Act
            var adminWasRemoved = _target.RemoveAdminId(444444);
            var actualListOfAdmin = _target.AdminIds;

            // Assert
            Assert.IsTrue(adminWasRemoved);
            CollectionAssert.AreEqual(expectedListOfAdmins, actualListOfAdmin);
        }

        /// <summary>
        /// Tests that the admin was successfully removed
        /// </summary>
        [TestMethod]
        public void GetAdminIds_TheExpectedIdsWasGot()
        {
            // Arrange
            _target.SetAdminId(111111);
            _target.SetAdminId(222222);
            _target.SetAdminId(333333);
            _target.SetAdminId(444444);

            var expectedListOfAdmins = new List<long>
            {
                111111,
                222222,
                333333,
                444444
            };

            // Act
            _target = new AdminIdsService(Environment.CurrentDirectory + "/TestData");
            var actualListOfAdmin = _target.AdminIds;

            // Assert
            CollectionAssert.AreEqual(expectedListOfAdmins, actualListOfAdmin);
        }

        /// <summary>
        /// Removes the file with the list of admins
        /// </summary>
        private void RemoveDataFile()
        {
            if (File.Exists(PathApp))
            {
                File.Delete(PathApp);
            }
        }
    }
}