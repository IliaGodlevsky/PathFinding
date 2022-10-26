﻿using Common.Extensions;
using Random.Interface;
using System;
using ValueRange;
using ValueRange.Enums;
using ValueRange.Extensions;

namespace Random.Realizations.Generators
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

        public uint NextUint()
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
            foreach (int i in (1, ArrayLength - 1))
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
                foreach (int i in (1, ArrayLength))
                {
                    int index = 1 + (i + CalculationConst) % (ArrayLength - 1);
                    seeds[i] -= seeds[index];
                }
            }
        }
    }
}