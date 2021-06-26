using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    public sealed class CanberraDistance : AbstractDistance, IHeuristic
    {
        private const int Precision = 2;

        public double Calculate(IVertex first, IVertex second)
        {
            return CalculateDistance(first, second);
        }

        protected override double Aggregate(IEnumerable<double> collection)
        {
            return Math.Round(collection.SumOrDefault(), Precision);
        }

        protected override double ZipMethod(int first, int second)
        {
            if (first == 0 && second == 0)
            {
                return default;
            }
            return Math.Abs(first - second) / (Math.Abs(first) + Math.Abs(second));
        }
    }
}
