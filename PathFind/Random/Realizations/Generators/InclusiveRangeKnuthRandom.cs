using Random.Interface;
using ValueRange;
using ValueRange.Extensions;

namespace System
{
    /// <summary>
    /// Knuth substractive generator
    /// </summary>
    public sealed class InclusiveRangeKnuthRandom : IRandom
    {
        private const int MBig = int.MaxValue;
        private const int MSeed = 161803398;
        private const int MZero = 0;
        private const int ArrayLength = 56;
        private const int InitializationConst = 21;
        private const int CalculationConst = 30;

        public InclusiveRangeKnuthRandom()
          : this(Environment.TickCount)
        {

        }

        public InclusiveRangeKnuthRandom(int seed)
        {
            seeds = new int[ArrayLength];
            this.seed = seed;
            lockObject = new object();
            Initialize();
            CalculateNumbers();
        }

        public int Next(int minValue, int maxValue)
        {
            lock (lockObject)
            {
                var range = new InclusiveValueRange<int>(maxValue, minValue);
                long module = (long)range.Amplitude() + 1;
                return (int)(Seed % module) + range.LowerValueOfRange;
            }
        }

        private int Seed
        {
            get
            {
                int result;
                if (++inext == ArrayLength)
                {
                    inext = 1;
                }
                if (++inextp == ArrayLength)
                {
                    inextp = 1;
                }
                result = seeds[inext] - seeds[inextp];
                if (result < MZero)
                {
                    result += MBig;
                }
                seeds[inext] = result;
                return result;
            }            
        }

        private void Initialize()
        {
            int ii, mj, mk;
            int subtraction = (seed == int.MinValue) ? int.MaxValue : Math.Abs(seed);
            mj = MSeed - subtraction;
            mj %= MBig;
            seeds[ArrayLength - 1] = mj;
            mk = 1;
            for (int i = 1; i < ArrayLength - 1; i++)
            {
                ii = (InitializationConst * i) % (ArrayLength - 1);
                seeds[ii] = mk;
                mk = mj - mk;
                if (mk < MZero)
                {
                    mk += MBig;
                }
                mj = seeds[ii];
            }
        }

        private void CalculateNumbers()
        {
            int limit = 5;
            while (limit-- > 1)
            {
                for (int i = 1; i < ArrayLength; i++)
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
            seed = 1;
        }

        private int inext;
        private int inextp;

        private int seed;
        private readonly int[] seeds;
        private readonly object lockObject;
    }
}