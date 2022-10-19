using Common.Extensions;
using Random.Interface;
using System;
using ValueRange;
using ValueRange.Enums;
using ValueRange.Extensions;

namespace Random.Realizations.Generators
{
    public sealed class KnuthRandom : IRandom
    {
        private const int MBig = int.MaxValue;
        private const int MSeed = 161803398;
        private const int MZero = 0;
        private const int ArrayLength = 56;
        private const int InitializationConst = 21;
        private const int CalculationConst = 30;

        private readonly InclusiveValueRange<int> indexRange;
        private readonly Lazy<int[]> seeds;

        private int inext;
        private int inextp;

        private int[] Seeds => seeds.Value;

        private int Seed
        {
            get
            {
                inext = indexRange.ReturnInRange(++inext, ReturnOptions.Cycle);
                inextp = indexRange.ReturnInRange(++inextp, ReturnOptions.Cycle);
                int result = Seeds[inext] - Seeds[inextp];
                if (result < MZero)
                {
                    result += MBig;
                }
                Seeds[inext] = result;
                return result;
            }
        }

        public KnuthRandom()
          : this(Environment.TickCount)
        {

        }

        public KnuthRandom(int seed)
        {
            seeds = new Lazy<int[]>(() => Initialize(seed), true);
            indexRange = new InclusiveValueRange<int>(ArrayLength - 1, 1);
        }

        public int Next(int minValue, int maxValue)
        {
            var range = new InclusiveValueRange<int>(maxValue, minValue);
            long module = (long)range.Amplitude() + 1;
            return (int)(Seed % module) + range.LowerValueOfRange;
        }

        private int[] Initialize(int seed)
        {
            var seeds = new int[ArrayLength];
            int seedIndex, mj, mk;
            mj = Math.Abs(MSeed - (seed == int.MinValue ? int.MaxValue : Math.Abs(seed)));
            seeds[ArrayLength - 1] = mj;
            mk = 1;
            foreach (int i in (1, ArrayLength - 1))
            {
                seedIndex = InitializationConst * i % (ArrayLength - 1);
                seeds[seedIndex] = mk;
                mk = mj - mk;
                if (mk < MZero)
                {
                    mk += MBig;
                }
                mj = seeds[seedIndex];
            }
            int limit = 5;
            while (limit-- > 1)
            {
                foreach (int i in (1, ArrayLength))
                {
                    seeds[i] -= seeds[1 + (i + CalculationConst) % (ArrayLength - 1)];
                    if (seeds[i] < MZero)
                    {
                        seeds[i] += MBig;
                    }
                }
            }
            inext = 0;
            inextp = CalculationConst + 1;
            return seeds;
        }
    }
}