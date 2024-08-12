using System;

namespace Pathfinding.Shared.Extensions
{
    public static class TimeSpanExtensions
    {
        public static void Wait(this TimeSpan timeToWait)
        {
            var start = DateTime.Now;
            var timePassed = DateTime.Now - start;
            while (timePassed < timeToWait)
            {
                timePassed = DateTime.Now - start;
            }
        }

        public static TimeSpan Multiply(this TimeSpan span, double value)
        {
            return TimeSpan.FromMilliseconds(span.TotalMilliseconds * value);
        }
    }
}
