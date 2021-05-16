﻿using Algorithm.Interfaces;
using Common.Extensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    public sealed class EuclidianDistance : BaseHeuristic, IHeuristic
    {
        private const int Precision = 1;
        private const double Power = 2;

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