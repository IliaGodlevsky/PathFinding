using System.Diagnostics;

namespace GraphLibrary.PauseMaker
{
    public delegate void Pause();
    public class Pauser
    {
        public Pause PauseEvent;

        public void Pause(int milliseconds)
        {
            var watch = new Stopwatch();
            watch.Start();
            while (watch.ElapsedMilliseconds < milliseconds)
                PauseEvent();
            watch.Stop();
        }
    }
}
