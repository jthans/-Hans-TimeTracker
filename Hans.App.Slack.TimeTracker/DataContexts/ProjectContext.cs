using Hans.App.Slack.TimeTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Hans.App.Slack.TimeTracker.DataContexts
{
    /// <summary>
    ///  DB Context that manages any tables associated to projects, or project management.  We'll keep these as granular
    ///     as we can, to promote simple changes down the road.
    /// </summary>
    public class ProjectContext : DbContext
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="ProjectContext" /> class - To read project data.
        /// </summary>
        /// <param name="options">Any options used to configure this context.</param>
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }

        // Projects Table
        public DbSet<Project> Projects { get; set; }

        // ProjectData Table
        public DbSet<ProjectData> ProjectData { get; set; }

        // Project/User Composite Table
        public DbSet<ProjectUser> ProjectUserAssociations { get; set; }

        #region Events

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set up the ProjectUser Composite Table.
            builder.Entity<ProjectUser>().HasKey(k => new { k.ProjectId, k.UserId });
            builder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany(u => u.Users).HasForeignKey(u => u.UserId);
            builder.Entity<ProjectUser>().HasOne(u => u.User).WithMany(p => p.Projects).HasForeignKey(p => p.ProjectId);
        }

        #endregion
    }
}
