using Hans.App.TimeTracker.DataContexts;
using Hans.App.TimeTracker.Interfaces;
using Hans.App.TimeTracker.Models;
using System;
using System.Linq;

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
        public Guid AddProject(AddProjectRequest addRequest)
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
            this._dbContext.SaveChanges();

            // TODO: Look into Refreshing Here.

            return newProject.Id;
        }

        /// <summary>
        ///  Pulls an organization, based on the name.
        /// </summary>
        /// <param name="organizationName">Name of the organization to search for.</param>
        /// <returns>The organization found by name, or null if none was found.</returns>
        public Organization GetOrganization(string organizationName)
        {
            return this._dbContext.Organizations.FirstOrDefault(x => x.Description == organizationName);
        }
    }
}
