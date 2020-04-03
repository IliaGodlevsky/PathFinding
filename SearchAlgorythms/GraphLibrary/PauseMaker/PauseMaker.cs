using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SearchAlgorythms.PauseMaker
{
    public static class PauseMaker
    {
        public static void WinFormsPause(int milliseconds)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < milliseconds)
                Application.DoEvents();
            sw.Stop();
        }

        public static void ConsolePause(int milliseconds)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < milliseconds)
                continue;
            sw.Stop();
        }
    }
}
