using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public sealed class ManhattanDistance : DistanceFunction, IHeuristic
    {
        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}