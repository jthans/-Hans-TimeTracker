using Hans.App.TimeTracker.Models;
using System;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.Interfaces
{
    /// <summary>
    ///  Time Tracing DAO that allows us to interact with EF Core, and our DB in order to save/retrieve/update data as
    ///     endpoints are processed in our API.
    /// </summary>
    public interface ITimeTrackerDAO
    {
        /// <summary>
        ///  Saves a new project, if it exists in the DB.  If the project requested already exists for the
        ///     workspace, we'll simply return the existing value.
        /// </summary>
        /// <param name="addRequest">Parameters needed to successfully create a new project.</param>
        /// <returns>The project ID that was created/existed.</returns>
        Task<Guid> AddProject(AddProjectRequest addRequest);

        /// <summary>
        ///  Adds project data, by adding a user logged activity.  
        /// </summary>
        /// <param name="startTrackingRequest">All information necessary to create a new ProjectData row.</param>
        /// <returns>The ID of the newly created row.</returns>
        Task<Guid> AddProjectData(StartTrackingRequest startTrackingRequest);
    }
}
