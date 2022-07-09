using System;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public sealed class ManhattanDistance : DistanceFunction
    {
        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}