using Pathfinding.Shared.Interface;

namespace Pathfinding.Shared.Random
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

        public uint NextUInt()
        {
            lock (syncRoot)
            {
                return random.NextUInt();
            }
        }
    }
}