using Common.Extensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.HeuristicFunctions
{
    public sealed class ManhattanDistance : BaseHeuristicFunction
    {
        protected override double Aggregate(IEnumerable<double> collection)
        {
            return collection.SumOrDefault();
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}