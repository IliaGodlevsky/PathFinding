using System.Diagnostics;

namespace GraphLibrary.Extensions
{
    public static class StopwatchExtension
    {
        public static string GetTimeInformation(this Stopwatch timer, string format)
        {
            return string.Format(format, timer.Elapsed.Minutes,
                    timer.Elapsed.Seconds, timer.Elapsed.Milliseconds);
        }
    }
}
