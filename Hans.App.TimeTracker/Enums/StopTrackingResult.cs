namespace Hans.App.TimeTracker.Enums
{
    /// <summary>
    ///  Enumeration that signifies the result of a StopTracking request.
    /// </summary>
    public enum StopTrackingResult
    {
        // Successful Start
        Success,

        // Unsuccessful Start
        Failure,

        // The given user has no projects being tracked.
        NoOpenProjects
    }
}
