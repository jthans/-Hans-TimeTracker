﻿// <auto-generated />
using System;
using Hans.App.Slack.TimeTracker.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hans.App.Slack.TimeTracker.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ExternalApplication");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.ProjectData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProjectId");

                    b.Property<DateTime>("TimeEnd");

                    b.Property<DateTime>("TimeStart");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectData");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExternalId");

                    b.Property<Guid?>("ProjectId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.Project", b =>
                {
                    b.HasOne("Hans.App.Slack.TimeTracker.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.ProjectData", b =>
                {
                    b.HasOne("Hans.App.Slack.TimeTracker.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Hans.App.Slack.TimeTracker.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Hans.App.Slack.TimeTracker.Models.User", b =>
                {
                    b.HasOne("Hans.App.Slack.TimeTracker.Models.Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
