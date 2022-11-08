using Shared.Random;
using System;

namespace Shared.Random.Realizations
{
    public sealed class PseudoRandom : IRandom
    {
        private const int Increment = 1;
        private const int Multiplier = 22695477;

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