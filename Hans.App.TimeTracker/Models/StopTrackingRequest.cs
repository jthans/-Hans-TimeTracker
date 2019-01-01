namespace Hans.App.TimeTracker.Models
{
    /// <summary>
    ///  Model containing all information necessary to stop tracking a project for a user.
    /// </summary>
    public class StopTrackingRequest
    {
        /// <summary>
        ///  Gets or sets the name of the organization that the user resides in.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        ///  Gets or sets the user ID that called the command.
        /// </summary>
        public string UserId { get; set; }
    }
}
