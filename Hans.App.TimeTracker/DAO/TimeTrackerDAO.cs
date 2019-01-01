using Hans.App.TimeTracker.DataContexts;
using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hans.App.TimeTracker.DAO
{
    /// <summary>
    ///  DAO used to interact with the database (EF Core supported), so that we can process all incoming requests.
    /// </summary>
    public class TimeTrackerDAO : ITimeTrackerDAO
    {
        #region Fields

        /// <summary>
        ///  DBContext to talk to the database when making calls.
        /// </summary>
        private readonly ProjectContext _dbContext;

        #endregion

        #region Constructors

        public TimeTrackerDAO(ProjectContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        /// <summary>
        ///  Saves a new project, if it exists in the DB.  If the project requested already exists for the
        ///     workspace, we'll simply return the existing value.
        /// </summary>
        /// <param name="addRequest">Parameters needed to successfully create a new project.</param>
        /// <returns>The project ID that was created/existed.</returns>
        public async Task<Guid> AddProject(AddProjectRequest addRequest)
        {
            // Get the organization we're working with.
            Organization workingOrg = this.GetOrganization(addRequest.OrganizationName);
            if (workingOrg == null)
            {
                Console.Error.WriteLine($"Organization { addRequest.OrganizationName } doesn't exist in our system.  Project won't be added.");
                return Guid.Empty;
            }

            // See if an existing project already exists for the database.
            var existingProject = this._dbContext.Projects.FirstOrDefault(x => x.Description == addRequest.ProjectName && x.Organization.Id == workingOrg.Id);
            if (existingProject != null)
            {
                Console.WriteLine($"Project { addRequest.ProjectName } already exists for organization { workingOrg.Id }. Project won't be added.");
                return existingProject.Id;
            }
            
            // Add the new project to the DB.
            Project newProject = new Project() { Description = addRequest.ProjectName, Organization = workingOrg };
            this._dbContext.Add(newProject);
            await this._dbContext.SaveChangesAsync();

            return newProject.Id;
        }

        /// <summary>
        ///  Adds project data, by adding a user logged activity.  
        /// </summary>
        /// <param name="startTrackingRequest">All information necessary to create a new ProjectData row.</param>
        /// <returns>The ID of the newly created row.</returns>
        public virtual async Task<Guid> AddProjectData(StartTrackingRequest startTrackingRequest)
        {
            // Get the organization we're working with.
            Organization workingOrg = this.GetOrganization(startTrackingRequest.OrganizationName);
            if (workingOrg == null)
            {
                Console.Error.WriteLine($"Organization { startTrackingRequest.OrganizationName } doesn't exist in our system.  Project won't be logged.");
                return Guid.Empty;
            }

            // Get the user we're working with.
            User workingUser = this.GetUser(startTrackingRequest.UserId);
            if (workingUser == null)
            {
                Console.Error.WriteLine($"User { startTrackingRequest.UserId } doesn't exist in our system.  Project won't be logged.");
                return Guid.Empty;
            }

            // Get the project we're working with.
            Project workingProject = this._dbContext.Projects.FirstOrDefault(x => x.Description == startTrackingRequest.ProjectName && x.Organization.Id == workingOrg.Id);
            if (workingProject == null)
            {
                Console.Error.WriteLine($"Project { startTrackingRequest.ProjectName } couldn't be found for organization { startTrackingRequest.OrganizationName }.  Project won't be logged.");
                return Guid.Empty;
            }

            // Add the new log to our system.
            ProjectData newData = new ProjectData() { Project = workingProject, User = workingUser, TimeStart = startTrackingRequest.StartTime };
            this._dbContext.Add(newData);
            await this._dbContext.SaveChangesAsync();
            
            return newData.Id;
        }

        /// <summary>
        ///  Finish tracking a particular activity record.
        /// </summary>
        /// <param name="activityId">Which activity we'd like to finish tracking.</param>
        /// <param name="timeFinished">The time the activity was finished.</param>
        public async Task FinishProjectData(Guid activityId, DateTime timeFinished)
        {
            var activityData = this._dbContext.ProjectData.FirstOrDefault(x => x.Id == activityId);
            activityData.TimeEnd = timeFinished;

            this._dbContext.Update(activityData);
            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///  See if the user has any open projects, if there aren't any open projects (no entries w/ EndTime NULL), we won't return anything.
        /// </summary>
        /// <param name="organizationName">Name of the organization the user exists within.</param>
        /// <param name="userName">Name of the user to search for.</param>
        /// <returns>The project data that's open, if any - Null if none.</returns>
        public virtual ProjectData FindOpenProject(string organizationName, string userName)
        {
            // Get the most recent ProjectData entry that has not been closed out (TimeEnd == NULL)
            return this._dbContext.ProjectData.Include(x => x.Project)
                                                .OrderByDescending(x => x.TimeStart)
                                                .FirstOrDefault(x => x.Project.Organization.Description == organizationName && 
                                                                        !x.TimeEnd.HasValue &&
                                                                        x.User.UserName == userName);
        }

        #region Accessors

        /// <summary>
        ///  Pulls an organization, based on the name.
        /// </summary>
        /// <param name="organizationName">Name of the organization to search for.</param>
        /// <returns>The organization found by name, or null if none was found.</returns>
        public virtual Organization GetOrganization(string organizationName)
        {
            return this._dbContext.Organizations.FirstOrDefault(x => x.Description == organizationName);
        }

        /// <summary>
        ///  Pulls a user, based on the name.
        /// </summary>
        /// <param name="userName">Name of the user we're searching for.</param>
        /// <returns>The user found by name, or null if none was found.</returns>
        public virtual User GetUser(string userName)
        {
            return this._dbContext.User.FirstOrDefault(x => x.UserName == userName);
        }

        #endregion
    }
}
