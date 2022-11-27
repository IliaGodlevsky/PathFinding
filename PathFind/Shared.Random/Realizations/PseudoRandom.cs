using System;

namespace Shared.Random.Realizations
{
    /// <summary>
    /// A random number generator based on linear congruential method
    /// </summary>
    public sealed class PseudoRandom : IRandom
    {
        private const uint Increment = 1;
        private const uint Multiplier = 22695477;

        private uint state;

        public PseudoRandom(int seed)
        {
            state = (uint)seed;
        }

        public PseudoRandom() : this(Environment.TickCount)
        {

        }

        public uint NextUint()
        {
            return state = state * Multiplier + Increment;
        }
    }
}