namespace GenericService.ServiceLogger
{
    /// <summary>
    /// The trace logger
    /// </summary>
    /// <seealso cref="GenericService.ServiceLogger.ILogger" />
    public class TraceLogger : ILogger
    {
        /// <summary>
        /// Logs the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public void Log(string category, string message, TraceLevel level)
        {
            System.Diagnostics.Trace.WriteLine(message, category);
        }
    }
}
