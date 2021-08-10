using System.Diagnostics;

namespace Common.Extensions
{
    public static class StopwatchExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="watch"></param>
        /// <param name="milliseconds"></param>
        /// <remarks>WARNING: This method doesn't start 
        /// stopwatch, stop or reset it. You should use 
        /// this method on a started stopwatch instance</remarks>
        public static void Wait(this Stopwatch watch, int milliseconds)
        {
            if (watch.IsRunning)
            {
                long currentMilliseconds = watch.ElapsedMilliseconds;
                long millisecondsPassed = watch.ElapsedMilliseconds - currentMilliseconds;
                while (millisecondsPassed < milliseconds)
                {
                    millisecondsPassed = watch.ElapsedMilliseconds - currentMilliseconds;
                }
            }
        }
    }
}
