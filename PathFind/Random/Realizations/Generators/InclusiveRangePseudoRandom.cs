using Random.Interface;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace Random.Realizations
{
    /// <summary>
    /// Linear congruential generator
    /// </summary>
    public sealed class InclusiveRangePseudoRandom : IRandom
    {
        private const int Term = 12345;
        private const int Factor = 1_103_515_245;

        private ulong Seed => seed = seed * Factor + Term;

        public InclusiveRangePseudoRandom(ulong seed)
        {
            this.seed = seed;
        }

        public InclusiveRangePseudoRandom()
            : this(((ulong)DateTime.UtcNow.Ticks))
        {

        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            ulong module = (ulong)range.Amplitude() + 1;
            return (int)(Seed % module) + range.LowerValueOfRange;
        }

        private ulong seed;
    }
}
