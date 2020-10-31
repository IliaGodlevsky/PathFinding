using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Сalculations.DistanceCalculating
{
    public static class DistanceCalculator
    {
        public static double GetChebyshevDistance(IVertex from, IVertex to)
        {
            if (from.Position.GetType() != to.Position.GetType() || ReferenceEquals(from, to))
                return 0;
            var fromCoordinates = from.Position.Coordinates.ToArray();
            var toCoordinates = to.Position.Coordinates.ToArray();
            var count = fromCoordinates.Length;
            var result = new List<int>();
            for (var i = 0; i < count; i++)
                result.Add(Math.Abs(fromCoordinates[i] - toCoordinates[i]));
            return result.Max();
        }
    }
}
