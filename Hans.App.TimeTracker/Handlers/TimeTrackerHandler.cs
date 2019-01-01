﻿using Hans.App.TimeTracker.Enums;
using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using System;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.Handlers
{
    /// <summary>
    ///  Handler that manages the time tracking business logic, anything that isn't data related but handles
    ///     what happens when certain requests come in happens here.
    /// </summary>
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

        /// <summary>
        ///  Stops tracking whatever project a particular user is currently working on.
        /// </summary>
        /// <param name="stopRequest">The user information to stop tracking information for.</param>
        /// <returns>The state of the stoppage, or NoProjectsOpen if no projects are available to stop tracking.</returns>
        public async Task<StopTrackingResult> StopTracking(StopTrackingRequest stopRequest)
        {
            // See if any projects are open for this user - If not, there's nothing to stop.
            var openProject = this._timeTrackerDAO.FindOpenProject(stopRequest.OrganizationName, stopRequest.UserId);
            if (openProject == null)
            {
                return StopTrackingResult.NoOpenProjects;
            }

            // Finish the project that's open for the user.  This will simply close it, at the time stop was called.
            try
            {
                await this._timeTrackerDAO.FinishProjectData(openProject.Id, DateTime.Now);
                return StopTrackingResult.Success;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error when stopping project { openProject.Id } for user { stopRequest.OrganizationName }/{ stopRequest.UserId }");
                return StopTrackingResult.Failure;
            }
        }

        #endregion
    }
}
