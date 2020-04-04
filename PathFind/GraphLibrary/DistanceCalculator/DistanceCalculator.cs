using SearchAlgorythms.Top;
using System;

namespace SearchAlgorythms.DistanceCalculator
{
    public static class DistanceCalculator
    {
        public static double GetEuclideanDistance(IGraphTop top1, IGraphTop top2)
        {
            return Math.Sqrt(Math.Pow(top1.Location.X - top2.Location.X, 2)
                + Math.Pow(top1.Location.Y - top2.Location.Y, 2));
        }

        public static double GetChebyshevDistance(IGraphTop top1, IGraphTop top2)
        {
            return Math.Max(Math.Abs(top1.Location.X - top2.Location.X),
                Math.Abs(top1.Location.Y - top2.Location.Y));
        }
    }
}
