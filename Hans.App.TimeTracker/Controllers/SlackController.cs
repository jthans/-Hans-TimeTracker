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

            AddProjectRequest addRequest = new AddProjectRequest { OrganizationName = slackRequest.TeamId, ProjectName = slackRequest.Text };
            return new JsonResult($"Project Added w/ ID: { this._timeTrackingHandler.AddProject(addRequest) }") { StatusCode = 200 };
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

            this._timeTrackingHandler.StartTracking(startRequest);
            return new JsonResult($"{ slackRequest.UserName } has started working on { startRequest.ProjectName }.") { StatusCode = 200 };
        }

        #endregion
    }
}
