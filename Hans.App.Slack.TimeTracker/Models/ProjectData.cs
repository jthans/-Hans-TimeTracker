using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hans.App.Slack.TimeTracker.Models
{
    /// <summary>
    ///  Class that represents a time entry into the tracker utility - It indicates the project being tracked,
    ///     the user that tracked it, and the start/end time. 
    /// </summary>
    public class ProjectData
    {
        /// <summary>
        ///  How long this activity lasted - Kept here, for simplicity of calculations.
        /// </summary>
        [NotMapped]
        public TimeSpan Duration { get { return this.TimeEnd - this.TimeStart; } }

        public Guid Id { get; set; }

        /// <summary>
        ///  Gets or sets the project associated with this entry.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        ///  Gets or sets the ending time of the activity.
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        ///  Gets or sets the start time of the activity.
        /// </summary>
        public DateTime TimeStart { get; set; }

        /// <summary>
        ///  Gets or sets the user that tracked this time.
        /// </summary>
        public User User { get; set; }
    }
}
