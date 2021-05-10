using Common.Interface;
using System;

namespace Common.Logging
{
    public sealed class Logs : ILog
    {
        public Logs(params ILog[] logs)
        {
            this.logs = logs;
        }

        public void Debug(string message)
        {
            foreach (var log in logs)
            {
                log.Debug(message);
            }
        }

        public void Error(Exception ex, string message = null)
        {
            foreach (var log in logs)
            {
                log.Error(ex, message);
            }
        }

        public void Error(string message)
        {
            foreach (var log in logs)
            {
                log.Error(message);
            }
        }

        public void Fatal(Exception ex, string message = null)
        {
            foreach (var log in logs)
            {
                log.Fatal(ex, message);
            }
        }

        public void Fatal(string message)
        {
            foreach (var log in logs)
            {
                log.Fatal(message);
            }
        }

        public void Info(string message)
        {
            foreach (var log in logs)
            {
                log.Info(message);
            }
        }

        public void Trace(string message)
        {
            foreach (var log in logs)
            {
                log.Trace(message);
            }
        }

        public void Warn(Exception ex, string message = null)
        {
            foreach (var log in logs)
            {
                log.Warn(ex, message);
            }
        }

        public void Warn(string message)
        {
            foreach (var log in logs)
            {
                log.Warn(message);
            }
        }

        private readonly ILog[] logs;
    }
}
