using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class IAccumulatedCostsExtensions
    {
        public static void ReevaluateIfNotExists(this IAccumulatedCosts costs, IVertex vertex, Func<IVertex, double> function, 
            double returnValueIfNotExists = double.PositiveInfinity)
        {
            if (costs.GetAccumulatedCost(vertex) == returnValueIfNotExists)
            {
                costs.Reevaluate(vertex, function(vertex));
            }
        }
    }
}
