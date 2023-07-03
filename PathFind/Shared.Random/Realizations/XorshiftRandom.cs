using System;

namespace Shared.Random.Realizations
{
    public sealed class XorshiftRandom : IRandom
    {
        private uint seed;

        public XorshiftRandom(uint seed)
        {
            if (seed == 0)
            {
                seed = 1;
            }

            this.seed = seed;
        }

        public XorshiftRandom() 
            : this((uint)Environment.TickCount)
        {

        }

        public uint NextUInt()
        {
            uint x = seed;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            seed = x;
            return x;
        }
    }
}
