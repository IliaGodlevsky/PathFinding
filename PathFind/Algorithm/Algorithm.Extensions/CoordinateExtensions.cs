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
            double result = default;
            for (int i = 0; i < self.CoordinatesValues.Length; i++)
            {
                result += (self.CoordinatesValues[i] * coordinate.CoordinatesValues[i]);
            }
            return result;
        }

        public static double GetVectorLength(this ICoordinate self)
        {
            double result = 0;
            for (int i = 0; i < self.CoordinatesValues.Length; i++)
            {
                result += (self.CoordinatesValues[i] * self.CoordinatesValues[i]);
            }
            return Math.Sqrt(result);
        }
    }
}
