using System;

namespace Hans.App.Slack.TimeTracker.Models
{
    /// <summary>
    ///  Class representing any over-arching "Organization" that collects groups of a developer in a
    ///     single space.  This lets us ensure projects or settings don't seep into other groups of developers,
    ///     and stays within the context of an application.
    /// </summary>
    public class Organization
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets the external application, which tells us which application this workspace belongs to/represents
        ///     with the developer space in the organization.
        ///   E.G. Slack, Teams, etc.
        /// </summary>
        public string ExternalApplication { get; set; }
    }
}
