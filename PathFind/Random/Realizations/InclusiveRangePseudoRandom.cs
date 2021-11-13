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
        private const long C = 12345;
        private const long A = 1_103_515_245;

        public InclusiveRangePseudoRandom(long seed)
        {
            this.seed = seed;
        }

        public InclusiveRangePseudoRandom()
        {
            seed = DateTime.Now.Ticks;
        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            long amplitude = (long)range.Amplitude() + 1;
            return amplitude == 0 ? range.LowerValueOfRange
                : (int)(Seed % amplitude)
                + range.LowerValueOfRange;
        }

        private long Seed => seed = Math.Abs(seed * A + C);

        private long seed;
    }
}
