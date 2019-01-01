using Hans.App.TimeTracker.Enums;
using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using Hans.Slack;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hans.App.TimeTracker.Controllers
{
    public class SlackController
    {
        #region Fields

        /// <summary>
        ///  Handler that is used to handle any incoming requests to the controller, each API endpoint should have
        ///     a method in this class to handle any requests.  Most of them will expect the <see cref="SlackRequest" /> to
        ///     be broken up beforehand.
        /// </summary>
        private readonly ITimeTrackerHandler _timeTrackingHandler;

        #endregion

        #region Constructors

        public SlackController(ITimeTrackerHandler timeTrackingHandler)
        {
            this._timeTrackingHandler = timeTrackingHandler;
        }

        #endregion

        #region Endpoints

        /// <summary>
        ///  Adds a project to the database, to be tracked in a particular workspace.
        /// </summary>
        /// <param name="slackRequest">The request information.</param>
        /// <returns>If the project was added, and its new ID if so.</returns>
        [HttpPost]
        [Route("api/slack/addproject")]
        public ActionResult AddProject(SlackRequest slackRequest)
        {
            // Make sure we're getting some information.
            if (slackRequest == null ||
                string.IsNullOrEmpty(slackRequest.Text))
            {
                return new JsonResult($"ERROR: No Data Passed.") { StatusCode = 400 };
            }

            AddProjectRequest addRequest = new AddProjectRequest
            {
                OrganizationName = slackRequest.TeamId,
                ProjectName = slackRequest.Text
            };

            this._timeTrackingHandler.AddProject(addRequest);
            return new JsonResult($"Project Added.") { StatusCode = 200 };
        }

        /// <summary>
        ///  Echoes a string back to the user that called the command. Used as an easy test to see if the endpoints are running.
        /// </summary>
        /// <param name="requestModel">The model containing all request information.</param>
        /// <returns>The string the user input, italicized.</returns>
        [HttpPost]
        [Route("api/slack/echo")]
        public ActionResult Echo(SlackRequest slackRequest)
        {
            return new JsonResult($"_{ slackRequest?.Text }_") { StatusCode = 200 };
        }

        /// <summary>
        ///  Echoes the request contents back out to the user, for debugging purposes.
        /// </summary>
        /// <param name="slackRequest">The request we're researching.</param>
        /// <returns>The request information.</returns>
        [HttpPost]
        [Route("api/slack/request")]
        public ActionResult Request([FromBody] SlackRequest slackRequest)
        {
            // Return the Model.
            return new JsonResult(slackRequest.ToString()) { StatusCode = 200 };
        }

        /// <summary>
        ///  Starts tracking a project for the calling user.  
        /// </summary>
        /// <param name="requestModel">The request information, containing user/organization/text information.</param>
        /// <returns>If the tracking was successful.</returns>
        [HttpPost]
        [Route("api/slack/starttrack")]
        public ActionResult StartTracking(SlackRequest slackRequest)
        {
            // Make sure we're getting some information.
            if (slackRequest == null ||
                string.IsNullOrEmpty(slackRequest.Text))
            {
                return new JsonResult($"ERROR: No Data Passed.") { StatusCode = 400 };
            }

            // Create the request object, and let the handler handle it.
            StartTrackingRequest startRequest = new StartTrackingRequest
            {
                OrganizationName = slackRequest.TeamId,
                ProjectName = slackRequest.Text,
                StartTime = DateTime.Now,
                UserId = slackRequest.UserId
            };

            // Determine the proper error message for the result calculated.
            var displayResult = "Project Started.";
            var trackingResult = this._timeTrackingHandler.StartTracking(startRequest).Result;
            switch (trackingResult)
            {
                case StartTrackingResult.Failure:
                    displayResult = "Project Start FAILED.";
                    break;
                case StartTrackingResult.ProjectAlreadyStarted:
                    displayResult = "Project Already Being Tracked.";
                    break;
                default:
                    break;
            }

            return new JsonResult(displayResult) { StatusCode = 200 };
        }

        /// <summary>
        ///  Gives the ability for Slack commands to be set up with parameters so users can set up personal commands allowing them to track their project
        ///     time and effort faster, with less typing.
        /// </summary>
        /// <param name="slackRequest">The Slack information, posted from the application.</param>
        /// <param name="userId">The override user ID - Used if tracking other people.</param>
        /// <param name="projectName">The name of the project to be tracked with this command.</param>
        /// <returns>If the start was successful.</returns>
        [HttpPost]
        [Route("api/slack/starttrack-wparams")]
        public ActionResult StartTrackingWithParams(SlackRequest slackRequest, [FromQuery] string userId = null, [FromQuery] string projectName = null)
        {
            // Make sure we're getting some information.
            if (slackRequest == null ||
                string.IsNullOrEmpty(slackRequest.Text))
            {
                return new JsonResult($"ERROR: No Data Passed.") { StatusCode = 400 };
            }

            // Save the parameters over the request, if applicable.
            if (!string.IsNullOrEmpty(userId))
            {
                slackRequest.UserId = userId;
            }

            if (!string.IsNullOrEmpty(projectName))
            {
                slackRequest.Text = projectName;
            }

            // Call StartTracking with our modified parameters.
            return this.StartTracking(slackRequest);
        }

        /// <summary>
        ///  Stops tracking the current project for a given user.
        /// </summary>
        /// <param name="slackRequest">The Slack information passed to this method.</param>
        /// <returns>If the tracking was successfully stopped.</returns>
        [HttpPost]
        [Route("api/slack/stoptrack")]
        public ActionResult StopTracking(SlackRequest slackRequest)
        {
            // Make sure we're getting some information.
            if (slackRequest == null)
            {
                return new JsonResult($"ERROR: No Data Passed.") { StatusCode = 400 };
            }

            // Create the request, and process it.
            StopTrackingRequest stopRequest = new StopTrackingRequest
            {
                OrganizationName = slackRequest.TeamId,
                UserId = slackRequest.UserId
            };

            // Determine the proper error message for the result calculated.
            var displayResult = "Project Stopped.";
            var trackingResult = this._timeTrackingHandler.StopTracking(stopRequest).Result;
            switch (trackingResult)
            {
                case StopTrackingResult.Failure:
                    displayResult = "Project Stop FAILED.";
                    break;
                case StopTrackingResult.NoOpenProjects:
                    displayResult = "User has no open projects.";
                    break;
                default:
                    break;
            }

            return new JsonResult(displayResult) { StatusCode = 200 };
        }

        /// <summary>
        ///  Stops Tracking for a user with given parameters, this allows control of multiple users with special commands.
        /// </summary>
        /// <param name="slackRequest">The Slack request that was sent to the API.</param>
        /// <param name="userId">The user that is no longer being tracked.</param>
        /// <returns>If the stop was successful.</returns>
        [HttpPost]
        [Route("api/slack/stoptrack-wparams")]
        public ActionResult StopTrackingWithParams(SlackRequest slackRequest, [FromQuery] string userId = null)
        {
            // Make sure we're getting some information.
            if (slackRequest == null)
            {
                return new JsonResult($"ERROR: No Data Passed.") { StatusCode = 400 };
            }

            // Save the parameters over the request, if applicable.
            if (!string.IsNullOrEmpty(userId))
            {
                slackRequest.UserId = userId;
            }

            // Stop Tracking with our new parameters.
            return this.StopTracking(slackRequest);
        }
        
        #endregion
    }
}
