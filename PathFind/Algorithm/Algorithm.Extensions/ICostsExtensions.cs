using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Extensions
{
    public static class ICostsExtensions
    {
        public static void ReevaluateIfNotExists(this ICosts costs, IVertex vertex, Func<IVertex, double> function)
        {
            if (!costs.Contains(vertex))
            {
                costs.Reevaluate(vertex, function(vertex));
            }
        }

        public static double GetCostOrDefault(this ICosts costs, IVertex vertex, double defaultValue = double.PositiveInfinity)
        {
            return costs.Contains(vertex) ? costs.GetCost(vertex) : defaultValue;
        }
    }
}
