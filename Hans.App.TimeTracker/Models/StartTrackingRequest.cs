using System;

namespace Hans.App.TimeTracker.Models
{
    /// <summary>
    ///  Model containing all information necessary to start tracking a user on a project.
    /// </summary>
    public class StartTrackingRequest
    {
        /// <summary>
        ///  Gets or sets the time that this request was initiated.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///  Gets or sets the name of the organization that the user resides in.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        ///  Gets or sets the name of the project that we'll start tracking.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///  Gets or sets the user ID that called the command.
        /// </summary>
        public string UserId { get; set; }
    }
}
