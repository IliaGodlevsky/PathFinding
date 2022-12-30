using System;

namespace Shared.Random.Realizations
{
    /// <summary>
    /// A random number generator based on linear congruential method
    /// </summary>
    public sealed class PseudoRandom : IRandom
    {
        private readonly uint increment;
        private readonly uint multiplier;

        private uint state;

        public PseudoRandom(int seed)
            : this(increment: 1, multiplier: 22695477, seed)
        {
            this.state = (uint)seed;
        }

        public PseudoRandom(uint increment, uint multiplier, int seed)
        {
            this.increment = increment;
            this.multiplier = multiplier;
            this.state = (uint)seed;
        }

        public PseudoRandom()
            : this(Environment.TickCount)
        {

        }

        public uint NextUInt()
        {
            return state = state * multiplier + increment;
        }
    }
}