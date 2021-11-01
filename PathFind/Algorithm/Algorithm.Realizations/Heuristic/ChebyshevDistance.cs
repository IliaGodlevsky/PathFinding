using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    /// <summary>
    /// Calculates chebyshev distance between two vertices.
    /// This class can't be inherited
    /// </summary>
    /// <remarks><see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/></remarks>
    public sealed class ChebyshevDistance : Distance, IHeuristic
    {
        protected override double Aggregate(IEnumerable<double> collection)
        {
            return collection.MaxOrDefault();
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Abs(first - second);
        }
    }
}