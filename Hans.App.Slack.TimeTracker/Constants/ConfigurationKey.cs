namespace Hans.App.Slack.TimeTracker.Constants
{
    /// <summary>
    ///  Class containing all configuration keys used in appsettings.json, so that we can easily access the values
    ///     anywhere in code, without triple checking what string was typed.
    /// </summary>
    public class ConfigurationKey
    {
        /// <summary>
        ///  DBConnectionString - The connection string used to connect to the DB.
        /// </summary>
        public const string DBConnectionString = "DBConnectionString";
    }
}
