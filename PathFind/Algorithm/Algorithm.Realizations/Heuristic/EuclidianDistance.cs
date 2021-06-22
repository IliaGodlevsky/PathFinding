using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
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
            var sum = collection.SumOrDefault();
            sum = Math.Sqrt(sum);
            sum = Math.Round(sum, Precision);
            return sum;
        }

        protected override double ZipMethod(int first, int second)
        {
            return Math.Pow(first - second, Power);
        }
    }
}