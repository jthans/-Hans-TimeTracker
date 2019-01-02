using Hans.App.TimeTracker.DAO;
using Hans.App.TimeTracker.DataContexts;
using Hans.App.TimeTracker.Enums;
using Hans.App.TimeTracker.Handlers;
using Hans.App.TimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.Test.Handlers
{
    /// <summary>
    ///  Test that covers the <see cref="TimeTrackerHandler" /> and ensures the business logic it returns is correct, according
    ///     to our goals in design.
    /// </summary>
    [TestClass]
    public class TimeTrackerHandlerTest
    {
        #region Fields

        /// <summary>
        ///  Mock logger to satisfy constructor.
        /// </summary>
        private static Mock<ILogger<TimeTrackerHandler>> log;
        
        /// <summary>
        ///  Mock DAO necessary for manipulating the handler's logic.
        /// </summary>
        private static Mock<TimeTrackerDAO> timeTrackerDao;

        #endregion

        #region Test Management

        /// <summary>
        ///  Initializes a test class with the proper parameters, so we have access to the same Mock objects.
        /// </summary>
        [ClassInitialize]
        public static void InitializeTests(TestContext testContext)
        {
            // Create depedencies for the DAO.
            DbContextOptions<ProjectContext> dbOptions = new DbContextOptions<ProjectContext>();
            var projectContext = new Mock<ProjectContext>(dbOptions);

            // Create and save the DAO.
            log = new Mock<ILogger<TimeTrackerHandler>>();
            timeTrackerDao = new Mock<TimeTrackerDAO>(projectContext.Object);
        }

        #endregion

        #region StartTracking

        /// <summary>
        ///  Ensures that when the project has already been started, it returns the proper status.
        /// </summary>
        [TestMethod]
        public void StartTracking_ProjectAlreadyStarted_Success()
        {
            // Setup the mock data, that returns the same project we'll be searching for.
            const string projName = "TEST_PROJ";

            // Return the test project name whenever we search for an open project.
            timeTrackerDao.Setup(x => x.FindOpenProject(It.IsAny<string>(),
                                                             It.IsAny<string>()))
                                            .Returns(new Models.ProjectData
                                            {
                                                Id = Guid.NewGuid(),
                                                Project = new Models.Project
                                                {
                                                    Id = Guid.NewGuid(),
                                                    Description = projName
                                                }
                                            });

            // Handle the tracking start - We should expect to see the ProjectAlreadyStarted result.
            var trackHandler = new TimeTrackerHandler(log.Object, timeTrackerDao.Object);

            var startTrackingRequest = new StartTrackingRequest { ProjectName = projName };
            Assert.AreEqual(StartTrackingResult.ProjectAlreadyStarted, trackHandler.StartTracking(startTrackingRequest).Result);
        }

        /// <summary>
        ///  Ensures that when invalid data is passed to the DAO and returned an empty guid, the handler will return
        ///     the proper information.
        /// </summary>
        [TestMethod]
        public void StartTracking_StartsTrackingFailure()
        {
            // Setup the open project to return null, so we don't have to stop anything.
            timeTrackerDao.Setup(x => x.FindOpenProject(It.IsAny<string>(),
                                                        It.IsAny<string>()))
                                            .Returns((ProjectData)null);

            // Setup the project data method to return a valid GUID, like the DAO added it.
            timeTrackerDao.Setup(x => x.AddProjectData(It.IsAny<StartTrackingRequest>()))
                          .Returns(Task.FromResult<Guid>(Guid.Empty));

            // Handle the tracking start - We should expect to see the Success result.
            var trackHandler = new TimeTrackerHandler(log.Object, timeTrackerDao.Object);

            var startRequest = new StartTrackingRequest { ProjectName = "TEST_PROJ" };
            Assert.AreEqual(StartTrackingResult.Failure, trackHandler.StartTracking(startRequest).Result);
        }

        /// <summary>
        ///  Ensures that with valid data, the start tracking action works properly.
        /// </summary>
        [TestMethod]
        public void StartTracking_StartsTrackingSuccessfully()
        {
            // Setup the open project to return null, so we don't have to stop anything.
            timeTrackerDao.Setup(x => x.FindOpenProject(It.IsAny<string>(),
                                                        It.IsAny<string>()))
                                            .Returns((ProjectData) null);

            // Setup the project data method to return a valid GUID, like the DAO added it.
            timeTrackerDao.Setup(x => x.AddProjectData(It.IsAny<StartTrackingRequest>()))
                          .Returns(Task.FromResult<Guid>(Guid.NewGuid()));

            // Handle the tracking start - We should expect to see the Success result.
            var trackHandler = new TimeTrackerHandler(log.Object, timeTrackerDao.Object);

            var startRequest = new StartTrackingRequest { ProjectName = "TEST_PROJ" };
            Assert.AreEqual(StartTrackingResult.Success, trackHandler.StartTracking(startRequest).Result);
        }

        #endregion
    }
}
