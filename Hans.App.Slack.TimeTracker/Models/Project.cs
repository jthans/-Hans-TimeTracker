using System;
using System.Collections.Generic;

namespace Hans.App.Slack.TimeTracker.Models
{
    /// <summary>
    ///  Class that represents a Project in our time-tracking system, this is an item that users can add
    ///     hours and time to as they track their time. While this can include things like Admin or Training time,
    ///     the termonology stands with our main focus.
    /// </summary>
    public class Project
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets the organization associated with this project - We only want projects associated with a 
        ///     particular organization to be viewable in our time tracking utility - This is how we limit it.
        /// </summary>
        public Organization Organization { get; set; } 

        /// <summary>
        ///  Gets or sets the users associated with this project, can typically be null for an organization to view ALL projects
        ///     assigned to it, but to simplify searches we can associate users with this project and they are the only ones that
        ///     can access it.
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
