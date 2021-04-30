using Algorithm.Interfaces;
using Common.Extensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    public sealed class EuclidianDistance : BaseHeuristic, IHeuristic
    {
        private const int Precision = 1;

        protected override double Aggregate(IEnumerable<double> collection)
        {
            return Math.Round(Math.Sqrt(collection.SumOrDefault()), Precision);
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, 2);
        }
    }
}