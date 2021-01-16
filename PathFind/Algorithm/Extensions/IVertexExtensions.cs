using Common.Extensions;
using GraphLib.Vertex.Interface;
using System;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IVertexExtensions
    {
        private static int CalculateAbsSub(int first, int second)
        {
            return Math.Abs(first - second);
        }

        /// <summary>
        /// Returns chebyshev distance to <paramref name="toVertex"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toVertex"></param>
        /// <returns>Chebyshev distance or 0 if one of 
        /// vertices doesn't have any coordinates values</returns>
        public static double CalculateChebyshevDistanceTo(this IVertex self, IVertex toVertex)
        {
            if (self == null || toVertex == null)
            {
                throw new ArgumentNullException("Argument can't be null");
            }

            return self.Position.CoordinatesValues
                .Zip(toVertex.Position.CoordinatesValues, CalculateAbsSub)
                .MaxOrDefault();
        }
    }
}
