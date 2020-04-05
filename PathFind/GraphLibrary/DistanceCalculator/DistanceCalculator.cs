using SearchAlgorythms.Top;
using System;

namespace SearchAlgorythms.DistanceCalculator
{
    public static class DistanceCalculator
    {
        public static double GetEuclideanDistance(IGraphTop from, IGraphTop to)
        {
            return Math.Sqrt(Math.Pow(from.Location.X - to.Location.X, 2)
                + Math.Pow(from.Location.Y - to.Location.Y, 2));
        }

        public static double GetChebyshevDistance(IGraphTop from, IGraphTop to)
        {
            return Math.Max(Math.Abs(from.Location.X - to.Location.X),
                Math.Abs(from.Location.Y - to.Location.Y));
        }
    }
}
