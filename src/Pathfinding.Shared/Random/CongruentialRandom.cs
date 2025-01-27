using Pathfinding.Shared.Interface;
using System;

namespace Pathfinding.Shared.Random
{
    /// <summary>
    /// A random number generator based on linear congruential method
    /// </summary>
    public sealed class CongruentialRandom : IRandom
    {
        private readonly uint increment;
        private readonly uint multiplier;

        private uint state;

        public CongruentialRandom(int seed)
            : this(increment: 1, multiplier: 22695477, seed)
        {
        }

        public CongruentialRandom(uint increment, uint multiplier, int seed)
        {
            this.increment = increment;
            this.multiplier = multiplier;
            state = (uint)seed;
        }

        public CongruentialRandom(uint increment, uint multiplier)
            : this(increment, multiplier, Environment.TickCount)
        {
        }

        public CongruentialRandom()
            : this(Environment.TickCount)
        {

        }

        public uint NextUInt()
        {
            return state = state * multiplier + increment;
        }
    }
}