using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.DistanceCalculator
{
    public static class Distance
    {
        public static double GetEuclideanDistance(IVertex from, IVertex to) 
            => Math.Sqrt(Math.Pow(from.Location.X - to.Location.X, 2)
                + Math.Pow(from.Location.Y - to.Location.Y, 2));

        public static double GetChebyshevDistance(IVertex from, IVertex to) 
            => Math.Max(Math.Abs(from.Location.X - to.Location.X),
                Math.Abs(from.Location.Y - to.Location.Y));
    }
}
