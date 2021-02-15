using Algorithm.Interfaces;
using System;
using System.Diagnostics;

namespace Algorithm.Realizations
{
    /// <summary>
    /// Intermits algorithm for some amount of time
    /// </summary>
    public class AlgorithmIntermit : IIntermit
    {
        public event Action OnIntermitted;

        public AlgorithmIntermit(int intermitDuration)
        {
            timer = new Stopwatch();
            this.intermitDuration = intermitDuration;
        }

        public void Intermit()
        {
            timer.Reset();
            timer.Start();

            while (timer.ElapsedMilliseconds < intermitDuration)
            {
                OnIntermitted?.Invoke();
            }

            timer.Stop();
        }

        private readonly Stopwatch timer;
        private readonly int intermitDuration;
    }
}
