using GraphLib.Vertex.Interface;
using System;
using System.Linq;

namespace Algorithm.Сalculations
{
    internal static class DistanceCalculator
    {
        private static int GetAbsoluteSubstract(int first, int second)
        {
            return Math.Abs(first - second);
        }

        internal static double CalculateChebyshevDistance(IVertex fromVertex, IVertex toVertex)
        {
            if (fromVertex == null || toVertex == null)
            {
                return 0;
            }

            var fromCoordinates = fromVertex.Position.Coordinates.ToArray();
            var toCoordinates = toVertex.Position.Coordinates.ToArray();

            if (fromCoordinates.Length != toCoordinates.Length)
            {
                return 0;
            }

            return fromCoordinates.Zip(toCoordinates, GetAbsoluteSubstract).Max();
        }
    }
}
