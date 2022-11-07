using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class ChebyshevDistance : Distance
    {
        protected override double AggregateMethod(double a, double b)
        {
            return Math.Max(a, b);
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}