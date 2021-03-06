﻿using Hans.App.TimeTracker.Controllers;
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
            var addResult = new SlackController(this.mockHandler.Object).AddProject(new Slack.SlackRequest { text = "TEST" }) as JsonResult;

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
            var echoResult = new SlackController(this.mockHandler.Object).Echo(new Slack.SlackRequest { text = "TEST" }) as JsonResult;

            Assert.IsNotNull(echoResult);
            Assert.AreEqual(200, echoResult.StatusCode);
        }

        #endregion

        #region Request
        
        /// <summary>
        ///  Ensures the echo endpoint successfully returns the expected result.
        /// </summary>
        [TestMethod]
        public void Request_ReturnsSuccess()
        {
            var echoResult = new SlackController(this.mockHandler.Object).Request(new Slack.SlackRequest { text = "TEST" }) as JsonResult;

            Assert.IsNotNull(echoResult);
            Assert.AreEqual(200, echoResult.StatusCode);
        }

        #endregion

        #region StartTracking

        /// <summary>
        ///  Ensures when no body is received, returns a bad request result.
        /// </summary>
        [TestMethod]
        public void StartTracking_NullRequest_Fails()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTracking(null) as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(400, startResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a request is received, but with no parameters, returns a BAD_REQUEST.
        /// </summary>
        [TestMethod]
        public void StartTracking_NullText_Fails()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTracking(new Slack.SlackRequest()) as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(400, startResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a valid request is passed, a good status code is returned.
        /// </summary>
        [TestMethod]
        public void StartTracking_ReturnsSuccess()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTracking(new Slack.SlackRequest { text = "TEST" }) as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(200, startResult.StatusCode);
        }

        /// <summary>
        ///  Ensures when no body is received, returns a bad request result.
        /// </summary>
        [TestMethod]
        public void StartTrackingWithParams_NullRequest_Fails()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTrackingWithParams(null) as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(400, startResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a request is received, but with no parameters, returns a BAD_REQUEST.
        /// </summary>
        [TestMethod]
        public void StartTrackingWithParams_NullText_Fails()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTrackingWithParams(new Slack.SlackRequest()) as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(400, startResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a valid request is passed, a good status code is returned.
        /// </summary>
        [TestMethod]
        public void StartTrackingWithParams_ReturnsSuccess()
        {
            var startResult = new SlackController(this.mockHandler.Object).StartTrackingWithParams(new Slack.SlackRequest { text = "TEST" }, "DevUser_01", "TestProj_01") as JsonResult;

            Assert.IsNotNull(startResult);
            Assert.AreEqual(200, startResult.StatusCode);
        }

        #endregion

        #region StopTracking

        /// <summary>
        ///  Ensures when no body is received, returns a bad request result.
        /// </summary>
        [TestMethod]
        public void StopTracking_NullRequest_Fails()
        {
            var stopResult = new SlackController(this.mockHandler.Object).StopTracking(null) as JsonResult;

            Assert.IsNotNull(stopResult);
            Assert.AreEqual(400, stopResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a valid request is passed, a good status code is returned.
        /// </summary>
        [TestMethod]
        public void StopTracking_ReturnsSuccess()
        {
            var stopResult = new SlackController(this.mockHandler.Object).StopTracking(new Slack.SlackRequest { text = "TEST" }) as JsonResult;

            Assert.IsNotNull(stopResult);
            Assert.AreEqual(200, stopResult.StatusCode);
        }

        /// <summary>
        ///  Ensures when no body is received, returns a bad request result.
        /// </summary>
        [TestMethod]
        public void StopTrackingWithParams_NullRequest_Fails()
        {
            var stopResult = new SlackController(this.mockHandler.Object).StopTrackingWithParams(null) as JsonResult;

            Assert.IsNotNull(stopResult);
            Assert.AreEqual(400, stopResult.StatusCode);
        }

        /// <summary>
        ///  Ensures that when a valid request is passed, a good status code is returned.
        /// </summary>
        [TestMethod]
        public void StopTrackingWithParams_ReturnsSuccess()
        {
            var stopResult = new SlackController(this.mockHandler.Object).StopTrackingWithParams(new Slack.SlackRequest { text = "TEST" }, "DevUser_01") as JsonResult;

            Assert.IsNotNull(stopResult);
            Assert.AreEqual(200, stopResult.StatusCode);
        }

        #endregion
    }
}
