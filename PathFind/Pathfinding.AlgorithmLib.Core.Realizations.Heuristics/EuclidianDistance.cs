using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class EuclidianDistance : Distance
    {
        private const int Precision = 1;
        private const double Power = 2;

        protected override double ProcessResult(double result)
        {
            return Math.Round(Math.Sqrt(result), Precision);
        }

        protected override double Zip(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}