using System.Diagnostics;

namespace GraphLibrary.PauseMaker
{
    public class PauseProvider : IPauseProvider
    {
        public  Pause PauseEvent { get; set; }

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
