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
        private const long Term = 12345;
        private const long Factor = 1_103_515_245;

        private long Seed => seed = Math.Abs(seed * Factor + Term);

        public InclusiveRangePseudoRandom(long seed)
        {
            this.seed = seed;
        }

        public InclusiveRangePseudoRandom()
        {
            seed = DateTime.UtcNow.Ticks;
        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            long module = (long)range.Amplitude() + 1;
            return (int)(Seed % module) + range.LowerValueOfRange;
        }

        private long seed;
    }
}
