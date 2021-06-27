using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    /// <summary>
    /// Calculates euclidian distance between two vertices
    /// </summary>
    /// <remarks><see cref="https://en.wikipedia.org/wiki/Euclidean_distance"/></remarks>
    public sealed class EuclidianDistance : AbstractDistance, IHeuristic
    {
        private const int Precision = 1;
        private const double Power = 2;

        public double Calculate(IVertex first, IVertex second)
        {
            return CalculateDistance(first, second);
        }

        protected override double Aggregate(IEnumerable<double> collection)
        {
            var aggregation = Math.Sqrt(collection.SumOrDefault());
            return Math.Round(aggregation, Precision);
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}