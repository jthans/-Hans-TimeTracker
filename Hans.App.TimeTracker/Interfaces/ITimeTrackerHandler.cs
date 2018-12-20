using Hans.App.TimeTracker.Models;
using System;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.Interfaces
{
    /// <summary>
    ///  Interface representing a handler focused on time-tracking.  Kept as an interface to encourage DI usage.
    /// </summary>
    public interface ITimeTrackerHandler
    {
        /// <summary>
        ///  Adds a project to the DB, if it doesn't already exist.  Regardless, returns the ID of the project in the DB, with
        ///     or without creation - Empty ID means something went wrong.
        /// </summary>
        /// <param name="addRequest">The parameters necessary to add a new project.</param>
        /// <returns>The project ID, or empty if unsuccessful.</returns>
        Task<Guid> AddProject(AddProjectRequest addRequest);

        /// <summary>
        ///  Starts tracking a project for a particular user.
        /// </summary>
        /// <param name="startRequest">All request information needed to handle the start.</param>
        /// <returns>Nothing, is async.</returns>
        Task<Guid> StartTracking(StartTrackingRequest startRequest);
    }
}
