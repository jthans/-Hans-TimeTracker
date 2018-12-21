using EntityFrameworkCoreMock;
using Hans.App.TimeTracker.DAO;
using Hans.App.TimeTracker.DataContexts;
using Hans.App.TimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hans.App.TimeTracker.Test.DAO
{
    /// <summary>
    ///  Class responsible for testing the accessor methods in the <see cref="TimeTrackerDAO" /> class.  These are
    ///     kept separate, as we can have a single set of test data available for all of them to test successfully.
    /// </summary>
    [TestClass]
    public class DAOAccessorTest
    {
        /// <summary>
        ///  Mock collection of data the tests will run off of to return test data.
        /// </summary>
        private static DbContextMock<ProjectContext> dbContext;

        /// <summary>
        ///  DAO that we're testing in this class.
        /// </summary>
        private static TimeTrackerDAO timeTrackerDao;

        #region Test Management

        /// <summary>
        ///  Ensures everything is set up correctly for the tests in this class to execute.
        /// </summary>
        [ClassInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            // Create test data that we'll read from.
            var orgsData = new[]
            {
                new Organization { Id = Guid.NewGuid(), Description = "DevOrg_01", ExternalApplication = "Test" }
            };

            var userData = new[]
            {
                new User { Id = Guid.NewGuid(), UserName = "DevUser_01", ExternalId = "Test_01" }
            };

            // Create the context, and load our test data.
            dbContext = new DbContextMock<ProjectContext>(new DbContextOptions<ProjectContext>());
            dbContext.CreateDbSetMock(x => x.Organizations, orgsData);
            dbContext.CreateDbSetMock(x => x.User, userData);

            // Create the DAO.
            timeTrackerDao = new TimeTrackerDAO(dbContext.Object);
        }

        #endregion

        #region Organizations

        /// <summary>
        ///  Ensures that when an organization does exist, we return it from the context.
        /// </summary>
        [TestMethod]
        public void GetOrganization_GetsValidOrganization()
        {
            var foundOrg = timeTrackerDao.GetOrganization("DevOrg_01");

            Assert.IsNotNull(foundOrg);
            Assert.AreEqual("DevOrg_01", foundOrg.Description);
        }

        /// <summary>
        ///  Ensures that when an organization doesn't exist, we return NULL from the context.
        /// </summary>
        [TestMethod]
        public void GetOrganization_ReturnsNullInvalidOrganization()
        {
            var notFoundOrg = timeTrackerDao.GetOrganization("DevOrg_02");

            Assert.IsNull(notFoundOrg);
        }

        #endregion

        #region Users

        /// <summary>
        ///  Ensures that when an user does exist, we return it from the context.
        /// </summary>
        [TestMethod]
        public void GetUser_GetsValidUser()
        {
            var foundUser = timeTrackerDao.GetUser("DevUser_01");

            Assert.IsNotNull(foundUser);
            Assert.AreEqual("DevUser_01", foundUser.UserName);
        }

        /// <summary>
        ///  Ensures that when an user doesn't exist, we return NULL from the context.
        /// </summary>
        [TestMethod]
        public void GetUser_ReturnsNullInvalidUser()
        {
            var notFoundUser = timeTrackerDao.GetUser("DevUser_02");

            Assert.IsNull(notFoundUser);
        }

        #endregion
    }
}
