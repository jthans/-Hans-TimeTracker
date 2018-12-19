using System;

namespace Hans.App.Slack.TimeTracker.Models
{
    /// <summary>
    ///  Class that represents a user outside of the time tracker application, using an application to push time-tracking
    ///     data into our databases.  
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        /// <summary>
        ///  Gets or sets the user name used in the external service.
        ///     Considerations: Maybe eventually people can choose their own usernames? (Multiple apps)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///  Gets or sets the "external ID", which is the ID to other apps such as Slack, Discord,
        ///     Teams, etc.  This allows us to easily parse the messages coming in from external services,
        ///     while still allowing us to manage our DB entries ourselves.
        /// </summary>
        public string ExternalId { get; set; }
    }
}
