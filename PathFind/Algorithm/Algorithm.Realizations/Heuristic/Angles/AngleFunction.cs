using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Proxy.Extensions;
using System;
using System.Linq;

namespace Algorithm.Realizations.Heuristic.Angles
{
    public sealed class AngleFunction : IHeuristic
    {
        private const double Radians = 57.2956;

        private readonly ICoordinate startingPoint;

        public AngleFunction(IVertex startingPoint)
        {
            this.startingPoint = startingPoint.Position;
        }

        public double Calculate(IVertex first, IVertex second)
        {
            var firstSubstract = Substract(first.Position, startingPoint);
            var secondSubstract = Substract(second.Position, startingPoint);
            double scalarProduct = GetScalarProduct(firstSubstract, secondSubstract);
            double firstVectorLength = GetVectorLength(firstSubstract);
            double secondVectorLength = GetVectorLength(secondSubstract);
            double vectorSum = firstVectorLength * secondVectorLength;
            double cosValue = vectorSum > 0 ? scalarProduct / vectorSum : 0;
            return Math.Round(Radians * Math.Acos(cosValue), digits: 3);
        }

        private static ICoordinate Substract(ICoordinate self, ICoordinate coordinate)
        {
            return self.Zip(coordinate, (x, y) => x - y).ToCoordinate();
        }

        private static double GetScalarProduct(ICoordinate self, ICoordinate coordinate)
        {
            double result = default;
            foreach (int i in (0, self.Count))
            {
                result += (self[i] * coordinate[i]);
            }
            return result;
        }

        private static double GetVectorLength(ICoordinate self)
        {
            double result = 0;
            foreach (int i in (0, self.Count))
            {
                result += (self[i] * self[i]);
            }
            return Math.Sqrt(result);
        }
    }
}