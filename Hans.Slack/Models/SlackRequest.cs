using System;
using System.Runtime.Serialization;

namespace Hans.Slack
{
    /// <summary>
    ///  Base model for Slack requests that contain all fields a Slack API would be sending to us on a command or webhook call.  Ideally, APIs that 
    ///     consume Slack services will accept *only* this data type.    
    /// </summary>
    [DataContract]
    public class SlackRequest
    {
        /// <summary>
        ///  The channel ID (Slack DB) the request originated from.
        /// </summary>
        [DataMember(Name = "channel_id")]
        public string channel_id { get; set; }

        /// <summary>
        ///  The name of the channel the request originated from.
        /// </summary>
        [DataMember(Name = "channel_name")]
        public string channel_name { get; set; }

        /// <summary>
        ///  The command that was called to trigger this API call.
        /// </summary>
        [DataMember(Name = "command")]
        public string command { get; set; }

        /// <summary>
        ///  The enterprise ID (Slack DB) the crequest originated from.
        /// </summary>
        [DataMember(Name = "enterprise_id")]
        public string enterprise_id { get; set; }

        /// <summary>
        ///  The enterprise name the request originated from.
        /// </summary>
        [DataMember(Name = "enterprise_name")]
        public string enterprise_name { get; set; }

        /// <summary>
        ///  The URL we should respond to (in the event any interact with the user is necessary, we'll have to call this uri with our payload, to capture 
        ///     further input from the user.
        /// </summary>
        [DataMember(Name = "response_url")]
        public string response_url { get; set; }

        /// <summary>
        ///  The team domain that the request originated from.
        /// </summary>
        [DataMember(Name = "team_domain")]
        public string team_domain { get; set; }

        /// <summary>
        ///  The team ID (Slack DB) that the request originated from.
        /// </summary>
        [DataMember(Name = "team_id")]
        public string team_id { get; set; }

        /// <summary>
        ///  Argubaly the most important part of the message - All text passed by the user.
        /// </summary>
        [DataMember(Name = "text")]
        public string text { get; set; }

        /// <summary>
        ///  Verification token - <i>Depreciated.</i>
        /// </summary>
        [DataMember(Name = "token")]
        public string token { get; set; }

        /// <summary>
        ///  ID (Slack DB) of the trigger that threw this command.
        /// </summary>
        [DataMember(Name = "trigger_id")]
        public string trigger_id { get; set; }

        /// <summary>
        ///  User Id (Slack DB) that requested the endpoint.
        /// </summary>
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        /// <summary>
        ///  User's name that requested the content.
        /// </summary>
        [DataMember(Name = "user_name")]
        public string user_name { get; set; }

        /// <summary>
        ///  Returns a String Representation of the Model.
        /// </summary>
        /// <returns>All Values Enumerated.</returns>
        public override string ToString()
        {
            string requestBody = $"Slack Request [{ DateTime.Now }]:\n";

            // Build the Model.
            requestBody += $"ChannelId: { this.channel_id }\n";
            requestBody += $"ChannelName: { this.channel_name }\n";
            requestBody += $"Command: { this.command }\n";
            requestBody += $"EnterpriseId: { this.enterprise_id }\n";
            requestBody += $"EnterpriseName: { this.enterprise_name }\n";
            requestBody += $"ResponseUrl: { this.response_url }\n";
            requestBody += $"TeamDomain: { this.team_domain }\n";
            requestBody += $"TeamId: { this.team_id }\n";
            requestBody += $"Text: { this.text }\n";
            requestBody += $"Token: { this.token }\n";
            requestBody += $"TriggerId: { this.trigger_id }\n";
            requestBody += $"UserId: { this.user_id }\n";
            requestBody += $"UserName: { this.user_name }\n";

            return requestBody;
        }
    }
}
