using System;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public sealed class EuclidianDistance : DistanceFunction
    {
        private const int Precision = 1;
        private const double Power = 2;

        protected override double ProcessResult(double result)
        {
            return Math.Round(Math.Sqrt(result), Precision);
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}