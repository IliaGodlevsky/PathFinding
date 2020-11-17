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

        }

        public void Pause(int delayTime)
        {
            var timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < delayTime)
            {
                PauseEvent?.Invoke();
            }

            timer.Stop();
        }
    }
}
