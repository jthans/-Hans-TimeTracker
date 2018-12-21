using Hans.App.TimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hans.App.TimeTracker.DataContexts
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

        // Organizations Table
        public virtual DbSet<Organization> Organizations { get; set; }

        // Projects Table
        public virtual DbSet<Project> Projects { get; set; }

        // ProjectData Table
        public virtual DbSet<ProjectData> ProjectData { get; set; }

        // Project/User Composite Table
        public virtual DbSet<ProjectUser> ProjectUserAssociations { get; set; }

        // User Table
        public virtual DbSet<User> User { get; set; }

        #region Events

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set up the ProjectUser Composite Table.
            builder.Entity<ProjectUser>().HasKey(k => new { k.ProjectId, k.UserId });
            builder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany(u => u.Users).HasForeignKey(u => u.UserId);
            builder.Entity<ProjectUser>().HasOne(u => u.User).WithMany(p => p.Projects).HasForeignKey(p => p.ProjectId);

#if DEBUG
            // Load Test Data.
            builder.Entity<Organization>().HasData(
                    new Organization { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1), Description = "DevOrg_01", ExternalApplication = "Slack" },
                    new Organization { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2), Description = "DevOrg_02", ExternalApplication = "Slack" }
            );

            builder.Entity<User>().HasData(
                    new User { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1), UserName = "DevUser_01", ExternalId = "User01" }
            );
#endif
        }

        #endregion
    }
}
