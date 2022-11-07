using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class ManhattanDistance : Distance
    {
        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}