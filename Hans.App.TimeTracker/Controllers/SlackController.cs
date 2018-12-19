using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using Hans.Slack;
using Microsoft.AspNetCore.Mvc;

namespace Hans.App.TimeTracker.Controllers
{
    public class SlackController
    {
        /// <summary>
        ///  Handler that is used to handle any incoming requests to the controller, each API endpoint should have
        ///     a method in this class to handle any requests.  Most of them will expect the <see cref="SlackRequest" /> to
        ///     be broken up beforehand.
        /// </summary>
        private readonly ITimeTrackerHandler _timeTrackingHandler;

        #region Constructors

        public SlackController(ITimeTrackerHandler timeTrackingHandler)
        {
            this._timeTrackingHandler = timeTrackingHandler;
        }

        #endregion

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
        public ActionResult Echo(SlackRequest requestModel)
        {
            return new JsonResult($"_{ requestModel.Text }_") { StatusCode = 200 };
        }
    }
}
