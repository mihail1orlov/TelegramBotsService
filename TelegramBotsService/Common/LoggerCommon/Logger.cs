using System;
using NLog;

namespace LoggerCommon
{
    public class Logger : ILogger
    {
        private readonly NLog.ILogger _logger;

        public Logger(NLog.ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dispose all targets, and shutdown logging.
        /// </summary>
        public void Shutdown()
        {
            LogManager.Shutdown();
        }

        /// <summary>
        /// Writes the diagnostic message at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }
    }
}