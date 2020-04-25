using System.Diagnostics;

namespace GraphLibrary.PauseMaker
{
    public abstract class PauseMaker
    {
        protected abstract void PauseEvent();

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
