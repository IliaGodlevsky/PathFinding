using System;
using System.Diagnostics;

namespace Common.Extensions
{
    public static class StopwatchExtensions
    {
        public static Stopwatch Wait(this Stopwatch watch, int milliseconds)
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
            return watch;
        }

        public static void Stop(this Stopwatch self, object sender, EventArgs e)
        {
            self.Stop();
        }

        public static void Restart(this Stopwatch self, object sender, EventArgs e)
        {
            self.Restart();
        }

        public static void Start(this Stopwatch self, object sender, EventArgs e)
        {
            self.Start();
        }

        public static string ToFormattedString(this Stopwatch self)
        {
            return self.Elapsed.ToString(@"mm\:ss\.fff");
        }
    }
}
