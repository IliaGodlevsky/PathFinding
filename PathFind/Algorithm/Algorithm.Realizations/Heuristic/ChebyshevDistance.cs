using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    /// <summary>
    /// Calculates chebyshev distance between to vertices
    /// </summary>
    /// <remarks><see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/></remarks>
    public sealed class ChebyshevDistance : Distance, IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            return CalculateDistance(first, second);
        }

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