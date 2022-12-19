using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Shared.Random.Realizations
{
    public sealed class KnuthRandom : IRandom
    {
        private const int ArrayLength = 56;
        private const uint InitializationConst = 21;
        private const int CalculationConst = 30;

        private readonly InclusiveValueRange<int> indexRange;
        private readonly Lazy<uint[]> seeds;

        private int inext, inextp;

        private uint[] Seeds => seeds.Value;

        public KnuthRandom() : this(Environment.TickCount)
        {

        }

        public KnuthRandom(int seed)
        {
            seeds = new Lazy<uint[]>(() => GenerateSeedsRange(seed), true);
            indexRange = new InclusiveValueRange<int>(ArrayLength - 1, 1);
        }

        public uint NextUInt()
        {
            inext = indexRange.ReturnInRange(++inext, ReturnOptions.Cycle);
            inextp = indexRange.ReturnInRange(++inextp, ReturnOptions.Cycle);
            return Seeds[inext] -= Seeds[inextp];
        }

        private uint[] GenerateSeedsRange(int seed)
        {
            var seeds = CreateSeeds((uint)seed);
            CombSeeds(seeds);
            inext = 0;
            inextp = CalculationConst + 1;
            return seeds;
        }

        private static uint[] CreateSeeds(uint initialSeed)
        {
            var seeds = new uint[ArrayLength];
            uint seedIndex, mj, mk = 1;
            const uint MSeed = 161803398;
            mj = MSeed - initialSeed;
            seeds[ArrayLength - 1] = mj;
            for (int i = 1; i < ArrayLength - 1; i++)
            {
                seedIndex = InitializationConst * (uint)i % (ArrayLength - 1);
                seeds[seedIndex] = mk;
                mk = mj - mk;
                mj = seeds[seedIndex];
            }
            return seeds;
        }

        private static void CombSeeds(uint[] seeds)
        {
            int limit = 5;
            while (limit-- > 1)
            {
                for (int i = 1; i < ArrayLength; i++)
                {
                    int index = 1 + (i + CalculationConst) % (ArrayLength - 1);
                    seeds[i] -= seeds[index];
                }
            }
        }
    }
}