using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.DistanceCalculating
{
    public static class DistanceCalculator
    {
        public static double GetEuclideanDistance(IVertex from, IVertex to) =>
            Math.Sqrt(Math.Pow(from.Position.X - to.Position.X, 2)
                    + Math.Pow(from.Position.Y - to.Position.Y, 2));

        public static double GetChebyshevDistance(IVertex from, IVertex to) =>
            Math.Max(Math.Abs(from.Position.X - to.Position.X),
                     Math.Abs(from.Position.Y - to.Position.Y));

        public static double GetManhattanDistance(IVertex from, IVertex to) =>
                  Math.Abs(from.Position.X - to.Position.X)
                + Math.Abs(from.Position.Y - to.Position.Y);
    }
}
