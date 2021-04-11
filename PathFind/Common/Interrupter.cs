using Common.Interface;
using System;
using System.Diagnostics;

namespace Common
{
    /// <summary>
    /// Waits for some amount of time
    /// </summary>
    public class Interrupter : ISuspendable
    {
        public event Action OnSuspended;

        public Interrupter()
        {
            timer = new Stopwatch();
        }

        public void Suspend(int waitDuration)
        {
            timer.Reset();
            timer.Start();

            while (timer.ElapsedMilliseconds < waitDuration)
            {
                OnSuspended?.Invoke();
            }

            timer.Stop();
        }

        private readonly Stopwatch timer;
    }
}
