using Common.Interface;
using System;
using System.Diagnostics;

namespace Algorithm.Realizations
{
    /// <summary>
    /// Waits for some amount of time
    /// </summary>
    public class Pause : ISuspendable
    {
        public event Action OnInterrupted;

        public Pause()
        {
            timer = new Stopwatch();
        }

        public void Suspend(int waitDuration)
        {
            timer.Reset();
            timer.Start();

            while (timer.ElapsedMilliseconds < waitDuration)
            {
                OnInterrupted?.Invoke();
            }

            timer.Stop();
        }

        private readonly Stopwatch timer;
    }
}
