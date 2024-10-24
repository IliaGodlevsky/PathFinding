using Pathfinding.Logging.Interface;
using Pathfinding.Shared.Extensions;
using System;

namespace Pathfinding.Logging.Loggers
{
    public sealed class Logs : ILog
    {
        private readonly ILog[] logs;

        public Logs(params ILog[] logs)
        {
            this.logs = logs;
        }

        public void Debug(string message)
        {
            logs.ForEach(log => log.Debug(message));
        }

        public void Error(Exception ex, string message = null)
        {
            logs.ForEach(log => log.Error(ex, message));
        }

        public void Error(string message)
        {
            logs.ForEach(log => log.Error(message));
        }

        public void Fatal(Exception ex, string message = null)
        {
            logs.ForEach(log => log.Fatal(ex, message));
        }

        public void Fatal(string message)
        {
            logs.ForEach(log => log.Fatal(message));
        }

        public void Info(string message)
        {
            logs.ForEach(log => log.Info(message));
        }

        public void Trace(string message)
        {
            logs.ForEach(log => log.Trace(message));
        }

        public void Warn(Exception ex, string message = null)
        {
            logs.ForEach(log => log.Warn(ex, message));
        }

        public void Warn(string message)
        {
            logs.ForEach(log => log.Warn(message));
        }
    }
}
