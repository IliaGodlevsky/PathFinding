using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Algorithm.Extensions
{
    public static class ICostsExtensions
    {
        /// <summary>
        /// Reassignes vertex's cost, using <paramref name="function"/>
        /// </summary>
        /// <param name="costs"></param>
        /// <param name="vertex">A vertex, which cost should be changed</param>
        /// <param name="function">A function, that used to generate cost</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReevaluateIfNotExists(this ICosts costs, IVertex vertex, Func<IVertex, double> function)
        {
            if (!costs.Contains(vertex))
            {
                costs.Reevaluate(vertex, function(vertex));
            }
        }

        /// <summary>
        /// Returns vertex's cost or <paramref name="defaultValue"/> if cost doesn't exist
        /// </summary>
        /// <param name="costs"></param>
        /// <param name="vertex">A vertex, which cost should be return</param>
        /// <param name="defaultValue">A value, that should be returned in case of absence of cost</param>
        /// <returns>Vertex's cost or <paramref name="defaultValue"/> if doesn't exist</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetCostOrDefault(this ICosts costs, IVertex vertex, double defaultValue = double.PositiveInfinity)
        {
            return costs.Contains(vertex) ? costs.GetCost(vertex) : defaultValue;
        }
    }
}
