using System;

namespace Common.Logger.Interface
{
    public interface ILog
    {
        void Log(Exception ex);
        void Log(string format, params object[] paramters);
    }
}
