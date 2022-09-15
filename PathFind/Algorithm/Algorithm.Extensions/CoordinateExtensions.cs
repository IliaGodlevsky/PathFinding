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
            return self.Zip(coordinate, (x, y) => x - y).ToCoordinate();
        }

        public static double GetScalarProduct(this ICoordinate self, ICoordinate coordinate)
        {
            double result = default;
            for (int i = 0; i < self.Count; i++)
            {
                result += (self[i] * coordinate[i]);
            }
            return result;
        }

        public static double GetVectorLength(this ICoordinate self)
        {
            double result = 0;
            for (int i = 0; i < self.Count; i++)
            {
                result += (self[i] * self[i]);
            }
            return Math.Sqrt(result);
        }
    }
}
