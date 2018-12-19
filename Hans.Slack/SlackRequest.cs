using System.Runtime.Serialization;

namespace Hans.Slack
{
    /// <summary>
    ///  Base model for Slack requests that contain all fields a Slack API would be sending to us on a command or webhook call.  Ideally, APIs that 
    ///     consume Slack services will accept *only* this data type.    /// </summary>
    public class SlackRequest
    {
        /// <summary>
        ///  The channel ID (Slack DB) the request originated from.
        /// </summary>
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        ///  The name of the channel the request originated from.
        /// </summary>
        [DataMember(Name = "channel_name")]
        public string ChannelName { get; set; }

        /// <summary>
        ///  The command that was called to trigger this API call.
        /// </summary>
        [DataMember(Name = "command")]
        public string Command { get; set; }

        /// <summary>
        ///  The enterprise ID (Slack DB) the crequest originated from.
        /// </summary>
        [DataMember(Name = "enterprise_id")]
        public string EnterpriseId { get; set; }

        /// <summary>
        ///  The enterprise name the request originated from.
        /// </summary>
        [DataMember(Name = "enterprise_name")]
        public string EnterpriseName { get; set; }

        /// <summary>
        ///  The URL we should respond to (in the event any interact with the user is necessary, we'll have to call this uri with our payload, to capture 
        ///     further input from the user.
        /// </summary>
        [DataMember(Name = "response_url")]
        public string ResponseUrl { get; set; }

        /// <summary>
        ///  The team domain that the request originated from.
        /// </summary>
        [DataMember(Name = "team_domain")]
        public string TeamDomain { get; set; }

        /// <summary>
        ///  The team ID (Slack DB) that the request originated from.
        /// </summary>
        [DataMember(Name = "team_id")]
        public string TeamId { get; set; }

        /// <summary>
        ///  Argubaly the most important part of the message - All text passed by the user.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        ///  Verification token - <i>Depreciated.</i>
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        ///  ID (Slack DB) of the trigger that threw this command.
        /// </summary>
        [DataMember(Name = "trigger_id")]
        public string TriggerId { get; set; }

        /// <summary>
        ///  User Id (Slack DB) that requested the endpoint.
        /// </summary>
        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        ///  User's name that requested the content.
        /// </summary>
        [DataMember(Name = "user_name")]
        public string UserName { get; set; }
    }
}
