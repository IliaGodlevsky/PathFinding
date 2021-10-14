﻿using Algorithm.Interfaces;
using Common.Extensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Realizations.Heuristic
{
    /// <summary>
    /// Calculates manhattan distance between two vertices.
    /// This class can't be inherited
    /// </summary>
    /// <remarks><see cref="https://xlinux.nist.gov/dads/HTML/manhattanDistance.html"/></remarks>
    public sealed class ManhattanDistance : Distance, IHeuristic
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