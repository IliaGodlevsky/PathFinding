using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic.Distances
{
    /// <summary>
    /// Calculates euclidian distance between two vertices.
    /// This class can't be inherited
    /// </summary>
    /// <remarks><see cref="https://en.wikipedia.org/wiki/Euclidean_distance"/></remarks>
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