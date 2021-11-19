using Random.Interface;
using ValueRange;
using ValueRange.Extensions;

namespace System
{
    /// <summary>
    /// Knuth substractive generator
    /// </summary>
    /// <remarks>See Numeric resipes in C, second edition</remarks>
    public sealed class KnuthRandom : IRandom
    {
        private const int MBig = int.MaxValue;
        private const int MSeed = 161803398;
        private const int MZero = 0;
        private const int ArrayLength = 56;
        private const int InitializationConst = 21;
        private const int CalculationConst = 30;

        private int[] Seeds => seeds.Value;

        /// <summary>
        /// Initializes new instance of <see cref="KnuthRandom"/>
        /// using <see cref="Environment.TickCount"/> as seed
        /// </summary>
        public KnuthRandom()
          : this(Environment.TickCount)
        {

        }

        public KnuthRandom(int seed)
        {
            seeds = new Lazy<int[]>(() => Initialize(seed));
            lockObject = new object();
        }

        /// <summary>
        /// Returns a pseudo random number 
        /// based on Knuth algorithm
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
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
                result = Seeds[inext] - Seeds[inextp];
                if (result < MZero)
                {
                    result += MBig;
                }
                Seeds[inext] = result;
                return result;
            }
        }

        private int[] Initialize(int seed)
        {
            var seeds = new int[ArrayLength];
            int seedIndex, mj, mk;
            mj = MSeed - ((seed == int.MinValue) ? int.MaxValue : Math.Abs(seed));
            mj %= MBig;
            seeds[ArrayLength - 1] = mj;
            mk = 1;
            for (int i = 1; i < ArrayLength - 1; i++)
            {
                seedIndex = (InitializationConst * i) % (ArrayLength - 1);
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
            return seeds;
        }

        private int inext;
        private int inextp;

        private readonly Lazy<int[]> seeds;
        private readonly object lockObject;
    }
}