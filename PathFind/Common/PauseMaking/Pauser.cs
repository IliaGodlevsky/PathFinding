using GraphLib.PauseMaking.Interface;
using System;
using System.Diagnostics;

namespace GraphLib.PauseMaking
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

        public PauseProvider()
        {
            timer = new Stopwatch();
        }

        public void Pause(int delayTime)
        {
            timer.Reset();
            timer.Start();

            while (timer.ElapsedMilliseconds < delayTime)
            {
                PauseEvent?.Invoke();
            }

            timer.Stop();
        }

        private readonly Stopwatch timer;
    }
}
