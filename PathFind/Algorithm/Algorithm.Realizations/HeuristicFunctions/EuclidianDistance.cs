using System;
using System.Collections.Generic;
using Common.Extensions;

namespace Algorithm.Realizations.HeuristicFunctions
{
    public sealed class EuclidianDistance : BaseHeuristicFunction
    {
        protected override double Aggregate(IEnumerable<double> collection)
        {
            return Math.Sqrt(collection.SumOrDefault());
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, 2);
        }
    }
}