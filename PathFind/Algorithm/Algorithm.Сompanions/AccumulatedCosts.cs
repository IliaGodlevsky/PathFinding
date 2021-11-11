using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class AccumulatedCosts : IAccumulatedCosts
    {
        public AccumulatedCosts(double returnIfNotExists = double.PositiveInfinity)
        {
            this.returnIfNotExists = returnIfNotExists;
            accumulatedCosts = new Dictionary<ICoordinate, double>();
        }

        public void Reevaluate(IVertex vertex, double accumulatedCost)
        {
            accumulatedCosts[vertex.Position] = accumulatedCost;
        }

        public double GetAccumulatedCost(IVertex vertex)
        {
            if (accumulatedCosts.TryGetValue(vertex.Position, out var cost))
            {
                return cost;
            }

            return returnIfNotExists;
        }

        public int Compare(IVertex vertex, double accumulatedCost)
        {
            var vertexAccumulatedCost = GetAccumulatedCost(vertex);
            return vertexAccumulatedCost.CompareTo(accumulatedCost);
        }

        public void Clear()
        {
            accumulatedCosts.Clear();
        }

        private readonly IDictionary<ICoordinate, double> accumulatedCosts;
        private readonly double returnIfNotExists;
    }
}