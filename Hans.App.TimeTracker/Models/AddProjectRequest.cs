namespace Hans.App.TimeTracker.Models
{
    /// <summary>
    ///  Model that contains critical information when adding new projects to the DB.
    /// </summary>
    public class AddProjectRequest
    {
        /// <summary>
        ///  Gets or sets the organization adding this project to their environment.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        ///  Gets or sets the project name that is being added.
        /// </summary>
        public string ProjectName { get; set; }
    }
}
