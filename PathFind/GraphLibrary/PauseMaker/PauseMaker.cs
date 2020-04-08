using System.Diagnostics;

namespace SearchAlgorythms.PauseMaker
{
    public abstract class PauseMaker
    {
        public abstract void PauseEvent();

        public virtual void Pause(int milliseconds)
        {
            var watch = new Stopwatch();
            watch.Start();
            while (watch.ElapsedMilliseconds < milliseconds)
                PauseEvent();
            watch.Stop();
        }
    }
}
