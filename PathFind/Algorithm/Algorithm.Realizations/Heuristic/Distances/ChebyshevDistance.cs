using Algorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.Heuristic.Distances
{
    public sealed class ChebyshevDistance : Distance, IHeuristic
    {
        protected override double Aggregate(IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}