using Common.Extensions;
using GraphLib.Exceptions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class VertexExtensions
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">Thrown when coordinate types differs or null</exception>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Chebyshev_distance"/></remarks>
        public static double CalculateChebyshevDistanceTo(this IVertex self, IVertex toVertex)
        {
            #region InvariantsObservance
            if (self == null || toVertex == null)
            {
                throw new ArgumentNullException();
            }

            if (self.Position == null || toVertex.Position == null)
            {
                throw new ArgumentException("Vertex coordinate was set to null");
            }

            if (self.Position.CoordinatesValues.Count() != toVertex.Position.CoordinatesValues.Count())
            {
                string message = "Can't calculate distance between vertices with different coordinates count";
                throw new WrongNumberOfDimensionsException(message);
            }
            #endregion

            return self
                .Position
                .CoordinatesValues
                .Zip(toVertex.Position.CoordinatesValues, CalculateAbsSub)
                .MaxOrDefault();
        }
    }
}
