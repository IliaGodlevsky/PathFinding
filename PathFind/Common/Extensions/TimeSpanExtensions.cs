using System;

namespace Common.Extensions
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
    }
}
