using Pathfinding.Shared.Interface;
using System;

namespace Pathfinding.Shared.Random
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
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;
            return seed;
        }
    }
}
