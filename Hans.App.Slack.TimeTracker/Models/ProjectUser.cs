using System;
using System.ComponentModel.DataAnnotations;

namespace Hans.App.Slack.TimeTracker.Models
{
    public class ProjectUser
    {
        /// <summary>
        ///  Project Association
        /// </summary>
        [Key]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        ///  User Association
        /// </summary>
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
