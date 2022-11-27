namespace Shared.Random.Realizations
{
    /// <summary>
    /// A thread safe decorator for random number generators, 
    /// that implement <see cref="IRandom"/> interface
    /// </summary>
    public sealed class ThreadSafeRandom : IRandom
    {
        private readonly IRandom random;
        private readonly object syncRoot;

        public ThreadSafeRandom(IRandom random)
        {
            this.random = random;
            syncRoot = new object();
        }

        public uint NextUint()
        {
            lock (syncRoot)
            {
                return random.NextUint();
            }
        }
    }
}