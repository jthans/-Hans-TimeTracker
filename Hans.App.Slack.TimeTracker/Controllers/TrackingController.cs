using Hans.Slack;
using Microsoft.AspNetCore.Mvc;

namespace Hans.App.Slack.TimeTracker.Controllers
{
    public class TrackingController
    {
        /// <summary>
        ///  Echoes a string back to the user that called the command.
        /// </summary>
        /// <param name="requestModel">The model containing all request information.</param>
        /// <returns>The string the user inout, italicized.</returns>
        [HttpPost]
        [Route("api/echo")]
        public string Echo([FromQuery]SlackRequest requestModel)
        {
            return $"_{ requestModel.Text }_";
        }
    }
}
