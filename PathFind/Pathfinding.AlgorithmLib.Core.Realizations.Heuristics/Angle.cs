using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Collections;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class Angle : IHeuristic
    {
        private const double Radians = 57.2956;

        private readonly ICoordinate startingPoint;

        public Angle(IVertex startingPoint)
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

        private static IReadOnlyList<int> Substract(IReadOnlyList<int> self, IReadOnlyList<int> coordinate)
        {
            var substractions = new int[self.Count];
            for (int i = 0; i < self.Count; i++)
            {
                substractions[i] = self[i] - coordinate[i];
            }
            return new ReadOnlyList<int>(substractions);
        }

        private static double GetScalarProduct(IReadOnlyList<int> self, IReadOnlyList<int> coordinate)
        {
            double result = default;
            for (int i = 0; i < self.Count; i++)
            {
                result += self[i] * coordinate[i];
            }
            return result;
        }

        private static double GetVectorLength(IReadOnlyList<int> self)
        {
            double result = 0;
            for (int i = 0; i < self.Count; i++)
            {
                result += self[i] * self[i];
            }
            return Math.Sqrt(result);
        }
    }
}