using Logging.Interface;
using NLog;
using System;

namespace Logging.Loggers
{
    public sealed class ConsoleLog : ILog
    {
        public void Trace(string message)
        {
            Write(message, consoleLogger.Trace, LogLevel.Trace);
        }

        public void Warn(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, consoleLogger.Warn, LogLevel.Warn);
        }

        public void Warn(string message)
        {
            Write(message, consoleLogger.Warn, LogLevel.Warn);

        }

        public void Error(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, consoleLogger.Error, LogLevel.Error);
        }

        public void Error(string message)
        {
            Write(message, consoleLogger.Error, LogLevel.Error);
        }

        public void Fatal(Exception ex, string message = null)
        {
            Write(ex.Message + "\n" + message, consoleLogger.Fatal, LogLevel.Fatal);
        }

        public void Fatal(string message)
        {
            Write(message, consoleLogger.Fatal, LogLevel.Fatal);
        }

        public void Info(string message)
        {
            Write(message, consoleLogger.Info, LogLevel.Info);
        }

        public void Debug(string message)
        {
            Write(message, consoleLogger.Debug, LogLevel.Debug);
        }

        private void Write(string message, Action<string> action, LogLevel logLevel)
        {
            bool isLogLevelAnaibled = consoleLogger.IsEnabled(logLevel);
            if (logLevel == LogLevel.Error
                || logLevel == LogLevel.Fatal
                && isLogLevelAnaibled)
            {
                //Console.Beep();
            }
            if (isLogLevelAnaibled)
            {
                Console.Clear();
            }
            action(message);
            if (isLogLevelAnaibled)
            {
                Console.ReadLine();
            }
        }

        public ConsoleLog()
        {
            consoleLogger = LogManager.GetLogger("Console");
        }

        private readonly ILogger consoleLogger;
    }
}
