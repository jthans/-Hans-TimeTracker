using Hans.App.TimeTracker.Constants;
using Hans.App.TimeTracker.DAO;
using Hans.App.TimeTracker.DataContexts;
using Hans.App.TimeTracker.Handlers;
using Hans.App.TimeTracker.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hans.App.TimeTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configure EF Core - Using MySQl Currently.
            services.AddDbContext<ProjectContext>(options => options.UseMySql(Configuration[ConfigurationKey.DBConnectionString]));

            // Configure Swagger to allow us to hit the API endpoints.
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Time Tracker API", Version = "v1" });
            });

            // Register Types
            services.AddTransient<ITimeTrackerDAO, TimeTrackerDAO>();
            services.AddTransient<ITimeTrackerHandler, TimeTrackerHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Configure Swagger to give us a visible UI to work in.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time Tracker API v1");
            });
        }
    }
}
