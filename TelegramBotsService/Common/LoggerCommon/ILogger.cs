using System;
using System.ComponentModel;

namespace LoggerCommon
{
    public interface ILogger
    {
        /// <summary>
        /// Dispose all targets, and shutdown logging.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Writes the diagnostic message at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        void Error(Exception exception, [Localizable(false)] string message);
    }
}