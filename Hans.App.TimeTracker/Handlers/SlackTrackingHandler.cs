using Hans.App.Slack.TimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hans.App.Slack.TimeTracker.Handlers
{
    public class SlackTrackingHandler
    {
        public Guid AddProject(AddProjectRequest addRequest)
        {
            return Guid.NewGuid();
        }
    }
}
