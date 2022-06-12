using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Proxy.Extensions;
using System;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class CoordinateExtensions
    {
        public static ICoordinate Substract(this ICoordinate self, ICoordinate coordinate)
        {
            return self.CoordinatesValues
                .Zip(coordinate.CoordinatesValues, (x, y) => x - y)
                .ToCoordinate();
        }

        public static double GetScalarProduct(this ICoordinate self, ICoordinate coordinate)
        {
            return self.CoordinatesValues
                .Zip(coordinate.CoordinatesValues, (a, b) => a * b)
                .SumOrDefault();
        }

        public static double GetVectorLength(this ICoordinate self)
        {
            return Math.Sqrt(self.CoordinatesValues.Select(x => x * x).SumOrDefault());
        }
    }
}
