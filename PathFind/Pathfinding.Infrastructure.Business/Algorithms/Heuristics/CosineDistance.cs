using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public sealed class CosineDistance : IHeuristic
    {
        private const double Radians = 57.2956;

        public double Calculate(IVertex first, IVertex second)
        {
            var firstVector = first.Position;
            var secondVector = second.Position;
            double scalarProduct = GetScalarProduct(firstVector, secondVector);
            double firstVectorLength = GetVectorLength(firstVector);
            double secondVectorLength = GetVectorLength(secondVector);
            double vectorSum = firstVectorLength * secondVectorLength;
            double cosine = vectorSum > 0 ? scalarProduct / vectorSum : 0;
            return Math.Round(Radians * Math.Acos(cosine), digits: 10);
        }

        private static double GetScalarProduct(IReadOnlyList<int> self,
            IReadOnlyList<int> coordinate)
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