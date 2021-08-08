using System.Diagnostics;

namespace Common.Extensions
{
    public static class StopwatchExtensions
    {
        public static void Wait(this Stopwatch watch, int milliseconds)
        {
            var currentMilliseconds = watch.ElapsedMilliseconds;
            while((watch.ElapsedMilliseconds - currentMilliseconds) < milliseconds)
            {

            }
        }
    }
}
