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

        private ulong seed;

        public PseudoRandom(int seed)
        {
            this.seed = (ulong)seed;
        }

        public PseudoRandom() : this(Environment.TickCount)
        {

        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            ulong module = (ulong)range.Amplitude() + 1;
            seed = seed * Factor + Term;
            return (int)(seed % module) + range.LowerValueOfRange;
        }
    }
}