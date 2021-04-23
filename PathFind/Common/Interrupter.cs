using Common.Interface;
using System;
using System.Diagnostics;

namespace Common
{
    /// <summary>
    /// Waits for some amount of time
    /// </summary>
    public sealed class Interrupter : ISuspendable
    {
        public event Action OnSuspended;

        public Interrupter(int waitDuration)
        {
            timer = new Stopwatch();
            this.waitDuration = waitDuration;
        }

        public void Suspend()
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
        private readonly int waitDuration;
    }
}
