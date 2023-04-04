using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class ChebyshevDistance : Distance
    {
        protected override double Aggregate(double a, double b)
        {
            return Math.Max(a, b);
        }

        protected override double Zip(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}