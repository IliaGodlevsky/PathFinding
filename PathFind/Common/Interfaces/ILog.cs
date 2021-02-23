using System;

namespace Common.Interfaces
{
    public interface ILog
    {
        void Trace(string message);

        void Warn(Exception ex, string message = null);

        void Error(Exception ex, string message = null);

        void Fatal(Exception ex, string message = null);

        void Info(string message);

        void Debug(string message);
    }
}
