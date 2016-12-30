namespace GenericService.ServiceLogger
{
    /// <summary>
    /// The ILogger logging contract
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        void Log(string category, string message, TraceLevel level);
    }

    /// <summary>
    /// The trace level enumeration
    /// </summary>
    public enum TraceLevel : int
    {
        /// <summary>
        /// The information
        /// </summary>
        Info = 0,
        /// <summary>
        /// The verbose
        /// </summary>
        Verbose,
        /// <summary>
        /// The error
        /// </summary>
        Error,
        /// <summary>
        /// The warning
        /// </summary>
        Warning,
        /// <summary>
        /// The fatal
        /// </summary>
        Fatal
    }
}
