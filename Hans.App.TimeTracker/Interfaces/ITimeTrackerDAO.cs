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

        /// <summary>
        ///  See if the user has any open projects, if there aren't any open projects (no entries w/ EndTime NULL), we won't return anything.
        /// </summary>
        /// <param name="organizationName">Name of the organization the user exists within.</param>
        /// <param name="userName">Name of the user to search for.</param>
        /// <returns>The project data that's open, if any - Null if none.</returns>
        ProjectData FindOpenProject(string organizationName, string userName);

        /// <summary>
        ///  Finish tracking a particular activity record.
        /// </summary>
        /// <param name="activityId">Which activity we'd like to finish tracking.</param>
        /// <param name="timeFinished">The time the activity was finished.</param>
        Task FinishProjectData(Guid activityId, DateTime timeFinished);
    }
}
