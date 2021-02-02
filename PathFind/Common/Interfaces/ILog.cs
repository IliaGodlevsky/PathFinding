using System;

namespace Common.Interfaces
{
    public interface ILog
    {
        void Log(Exception ex);
        void Log(string format, params object[] paramters);
    }
}
