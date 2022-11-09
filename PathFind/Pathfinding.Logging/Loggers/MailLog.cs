using NLog;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.Logging.Loggers
{
    public sealed class MailLog : ILog
    {
        public void Debug(string message)
        {
            mailLogger.Debug(message);
        }

        public void Error(Exception ex, string message = null)
        {
            mailLogger.Error(ex, message);
        }

        public void Error(string message)
        {
            mailLogger.Error(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            mailLogger.Fatal(ex, message);
        }

        public void Fatal(string message)
        {
            mailLogger.Fatal(message);
        }

        public void Info(string message)
        {
            mailLogger.Info(message);
        }

        public void Trace(string message)
        {
            mailLogger.Trace(message);
        }

        public void Warn(Exception ex, string message = null)
        {
            mailLogger.Warn(ex, message);
        }

        public void Warn(string message)
        {
            mailLogger.Warn(message);
        }

        public MailLog()
        {
            mailLogger = LogManager.GetLogger("Mail");
        }

        private readonly ILogger mailLogger;
    }
}
