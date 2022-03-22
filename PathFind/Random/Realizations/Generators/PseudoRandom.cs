using Random.Interface;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Realizations.Generators
{
    public sealed class PseudoRandom : IRandom
    {
        private const int Term = 12345;
        private const int Factor = 1103515245;

        private static readonly object locker = new object();

        private ulong seed;
        private ulong Seed => seed = seed * Factor + Term;

        public PseudoRandom(int seed)
        {
            this.seed = (ulong)seed;
        }

        public PseudoRandom()
            : this(Environment.TickCount)
        {

        }

        public int Next(int minValue, int maxValue)
        {
            lock (locker)
            {
                var range = new InclusiveValueRange<int>(maxValue, minValue);
                ulong module = (ulong)range.Amplitude() + 1;
                return (int)(Seed % module) + range.LowerValueOfRange;
            }
        }
    }
}