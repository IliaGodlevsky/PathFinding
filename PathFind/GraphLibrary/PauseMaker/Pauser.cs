using GraphLibrary.PauseMaker.Interface;
using System.Diagnostics;

namespace GraphLibrary.PauseMaker
{
    public class PauseProvider : IPauseProvider
    {
        public Pause PauseEvent { get; set; }

        public PauseProvider(int delayTime)
        {
            this.delayTime = delayTime;
        }

        public void Pause()
        {
            var timer = new Stopwatch();
            timer.Start();
            while (timer.ElapsedMilliseconds < delayTime)
                PauseEvent();
            timer.Stop();
        }

        private readonly int delayTime;
    }
}
