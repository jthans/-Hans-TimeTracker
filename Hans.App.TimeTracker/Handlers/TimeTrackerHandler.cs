using Hans.App.TimeTracker.Enums;
using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using System;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.Handlers
{
    public class TimeTrackerHandler : ITimeTrackerHandler
    {
        #region Fields

        /// <summary>
        ///  DAO used to interact with the DB as we handle incoming requests.
        /// </summary>
        private readonly ITimeTrackerDAO _timeTrackerDAO;

        #endregion

        #region Constructors

        public TimeTrackerHandler(ITimeTrackerDAO timeTrackerDAO)
        {
            this._timeTrackerDAO = timeTrackerDAO;
        }

        #endregion

        #region Endpoint Handlers

        /// <summary>
        ///  Adds a project to the DB, if it doesn't already exist.  Regardless, returns the ID of the project in the DB, with
        ///     or without creation - Empty ID means something went wrong.
        /// </summary>
        /// <param name="addRequest">The parameters necessary to add a new project.</param>
        /// <returns>The project ID, or empty if unsuccessful.</returns>
        public async Task<Guid> AddProject(AddProjectRequest addRequest)
        {
            return await this._timeTrackerDAO.AddProject(addRequest);
        }

        /// <summary>
        ///  Starts tracking a project for a particular user.
        /// </summary>
        /// <param name="startRequest">All request information needed to handle the start.</param>
        /// <returns>Nothing, is async.</returns>
        public async Task<StartTrackingResult> StartTracking(StartTrackingRequest startRequest)
        {
            // See if any projects are open for this user - If so, we'll need to stop tracking that project.
            var openProject = this._timeTrackerDAO.FindOpenProject(startRequest.OrganizationName, startRequest.UserId);
            if (openProject != null)
            {
                if (openProject.Project.Description == startRequest.ProjectName)
                {
                    return StartTrackingResult.ProjectAlreadyStarted;
                }

                // This is a different project - Stop tracking this project.
                await this._timeTrackerDAO.FinishProjectData(openProject.Id, startRequest.StartTime);
            }

            return await this._timeTrackerDAO.AddProjectData(startRequest) != Guid.Empty ? StartTrackingResult.Success : StartTrackingResult.Failure;
        }

        #endregion
    }
}
