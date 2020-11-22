using GraphLib.Vertex.Interface;
using System;
using System.Linq;

namespace Algorithm.Сalculations
{
    internal static class DistanceCalculator
    {
        internal static double CalculateChebyshevDistance(IVertex from, IVertex to)
        {
            if (from == null || to == null)
            {
                return 0;
            }

            var fromCoordinates = from.Position.Coordinates.ToArray();
            var toCoordinates = to.Position.Coordinates.ToArray();

            if (fromCoordinates.Length != toCoordinates.Length)
            {
                return 0;
            }

            var count = fromCoordinates.Length;
            var result = new int[count];

            for (var i = 0; i < count; i++)
            {
                var distanceBetween = fromCoordinates[i] - toCoordinates[i];
                result[i] = Math.Abs(distanceBetween);
            }

            return result.Max();
        }
    }
}
