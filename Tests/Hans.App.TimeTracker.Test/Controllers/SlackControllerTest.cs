using Hans.App.TimeTracker.Controllers;
using Hans.App.TimeTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hans.App.TimeTracker.Test.Controllers
{
    /// <summary>
    ///  Ensures all endpoints on the <see cref="SlackController" /> are valid, and behave as expected for the design.
    /// </summary>
    [TestClass]
    public class SlackControllerTest
    {
        private Mock<ITimeTrackerHandler> mockHandler;

        public SlackControllerTest()
        {
            this.mockHandler = new Mock<ITimeTrackerHandler>();
        }

        #region AddProject

        /// <summary>
        ///  Ensures that a null request returns a BAD_REQUEST.
        /// </summary>
        [TestMethod]
        public void AddProject_NullRequest_Fails()
        {
            var addResult = new SlackController(this.mockHandler.Object).AddProject(null) as JsonResult;

            Assert.IsNotNull(addResult);
            Assert.AreEqual(400, addResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a request is received, but with no parameters, returns a BAD_REQUEST.
        /// </summary>
        [TestMethod]
        public void AddProject_NullText_Fails()
        {
            var addResult = new SlackController(this.mockHandler.Object).AddProject(new Slack.SlackRequest()) as JsonResult;

            Assert.IsNotNull(addResult);
            Assert.AreEqual(400, addResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a valid request is passed, a good status code is returned.
        /// </summary>
        [TestMethod]
        public void AddProject_ReturnsSuccess()
        {
            var addResult = new SlackController(this.mockHandler.Object).AddProject(new Slack.SlackRequest { Text = "TEST" }) as JsonResult;

            Assert.IsNotNull(addResult);
            Assert.AreEqual(200, addResult.StatusCode);
        }

        #endregion

        #region Echo

        /// <summary>
        ///  Ensures the echo endpoint successfully returns the expected result.
        /// </summary>
        [TestMethod]
        public void Echo_ReturnsSuccess()
        {
            var echoResult = new SlackController(this.mockHandler.Object).Echo(new Slack.SlackRequest { Text = "TEST" }) as JsonResult;

            Assert.IsNotNull(echoResult);
            Assert.AreEqual(200, echoResult.StatusCode);
        }

        #endregion
    }
}
