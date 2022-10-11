using Random.Interface;

namespace Random.Realizations.Generators
{
    public sealed class ThreadSafeRandom : IRandom
    {
        private readonly IRandom random;
        private readonly object locker;

        public ThreadSafeRandom(IRandom random)
        {
            this.random = random;
            locker = new object();
        }

        public int Next(int minValue, int maxValue)
        {
            lock (locker)
            {
                return random.Next(minValue, maxValue);
            }
        }
    }
}