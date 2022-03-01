using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public sealed class EuclidianDistance : Distance, IHeuristic
    {
        private const int Precision = 1;
        private const double Power = 2;

        protected override double Aggregate(IEnumerable<double> collection)
        {
            double aggregation = Math.Sqrt(collection.SumOrDefault());
            return Math.Round(aggregation, Precision);
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}