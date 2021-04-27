using System;
using System.Collections.Generic;
using Algorithm.Interfaces;
using Common.Extensions;

namespace Algorithm.Realizations.Heuristic
{
    public sealed class ManhattanDistance : BaseHeuristic, IHeuristic
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