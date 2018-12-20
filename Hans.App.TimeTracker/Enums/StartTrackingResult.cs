namespace Hans.App.TimeTracker.Enums
{
    /// <summary>
    ///  Results possible when tracking has been started.
    /// </summary>
    public enum StartTrackingResult
    {
        // Project has already been started, no need to do it again.
        ProjectAlreadyStarted,

        // Successful Start.
        Success,

        // Unsuccessful Start.
        Failure
    }
}
