using GraphLibrary.PauseMaker.Interface;
using System;
using System.Diagnostics;

namespace GraphLibrary.PauseMaker
{
    /// <summary>
    /// Pauses all processes in current thread
    /// </summary>
    public class PauseProvider : IPauseProvider
    {
        /// <summary>
        /// Event during the pause
        /// </summary>
        public event Action PauseEvent;

        public PauseProvider(int delayTime)
        {
            this.delayTime = delayTime;
        }

        public void Pause()
        {
            var timer = new Stopwatch();
            timer.Start();
            while (timer.ElapsedMilliseconds < delayTime)
                PauseEvent?.Invoke();
            timer.Stop();
        }

        private readonly int delayTime;
    }
}
