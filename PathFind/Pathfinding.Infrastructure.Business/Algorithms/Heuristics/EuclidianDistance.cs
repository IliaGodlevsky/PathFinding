using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public sealed class EuclidianDistance : Distance
    {
        private readonly int Precision = 1;
        private readonly double Power = 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double ProcessResult(double result)
        {
            return Math.Round(Math.Sqrt(result), Precision);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Zip(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}